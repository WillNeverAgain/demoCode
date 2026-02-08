//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Refs;
using MyFrame.FightSystem.SkillV2.Actions;
using MyFrame.FightSystem.SkillV2.BT;
using MyFrame.FightSystem.SkillV2.Configs;
using MyFrame.FightSystem.SkillV2.Predicates;
using MyFrame.FightSystem.SkillV2.Targeting;
using MyFrame.FightSystem.SkillV2.Refs;
using System;
using System.Collections.Generic;
using System.Text;
using MyFrame.EventSystem.Events;

namespace MyFrame.FightSystem.SkillV2.Runtime
{
    /// <summary>
    /// Table-driven Skill Runtime (V2).
    /// 
    /// Config source:
    /// - Recommended: <see cref="Configs.SkillConfigData"/> loaded from JSON (see SkillV2Json).
    /// - Authoring can be Excel -> JSON (recommended production pipeline).
    /// 
    /// Design rules (hard constraints):
    /// 1) Skill-related operations MUST NOT bypass <see cref="Configs.EffectDef"/>.
    ///    Skill/Phase/Stage only orchestrate. They never directly mutate world state.
    /// 2) <see cref="Actions"/> are the minimal execution units. Only actions are allowed to buffer events.
    /// 3) Each Effect contains a BehaviourTree. Control flow lives in BT (And/Or/Not/If/ForEachTarget).
    /// 4) Execution order is strictly: Phase -> Stage -> (Effects buffer events) -> Release -> Next stage.
    ///    That means: world-state changes from a stage become visible ONLY after Release, i.e. in later stages.
    /// </summary>
    public sealed class SkillRuntimeV2
    {
        public SkillConfigData Config { get; }
        public Unit Host { get; }
        public uint CurrentCD { get; private set; }

        private readonly SkillActionRegistry _actions;

        public SkillRuntimeV2(SkillConfigData config, Unit host, SkillActionRegistry actions = null)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            Host = host ?? throw new ArgumentNullException(nameof(host));
            _actions = actions ?? SkillActionRegistry.CreateDefault();
        }

        /// <summary>
        /// Validate whether this skill can be executed now.
        /// NOTE: This method must be side-effect free (no EventBuffer writes).
        /// </summary>
        public SkillV2CheckReport CheckAccess(SkillV2ExecuteRequest request, SkillV2RuntimeContext world)
        {
            if (Config is null) return new SkillV2CheckReport(false, "Config is null");
            if (world is null) return new SkillV2CheckReport(false, "World context is null");

            // No events should be buffered during access check; but we still provide a buffer instance for eval safety.
            var dummy = new EventBuffer(world.EventBus);
            var eval = new SkillV2EvalContext(Config.skillId, CurrentCD, Host, request.Center, Array.Empty<Unit>(), null,
                world.Map, world.EventBus, dummy, world.Rng);

            bool ok = PredicateEvaluator.Eval(Config.accessCondition, eval);
            return ok
                ? new SkillV2CheckReport(true, "OK")
                : new SkillV2CheckReport(false, "Access denied by accessCondition");
        }

        /// <summary>
        /// Execute skill in a single call.
        /// It runs phases/stages sequentially. Each stage buffers events then releases them in batch.
        /// </summary>
        public SkillV2Report Execute(SkillV2ExecuteRequest request, SkillV2RuntimeContext world)
        {
            var check = CheckAccess(request, world);
            if (!check.CanAccess)
                return new SkillV2Report(false, $"SkillV2[{Config.skillId}] Access Denied: {check.Reason}");

            var sb = new StringBuilder(2048);
            sb.AppendLine($"SkillV2 Execute: {Config.skillId} host={Host.UnitId} center={(request.Center?.UnitId ?? "null")}");

            if (Config.phases is null || Config.phases.Count == 0)
                return new SkillV2Report(false, $"SkillV2[{Config.skillId}] no phases configured");

            try
            {
                for (int p = 0; p < Config.phases.Count; p++)
                {
                    var phase = Config.phases[p];
                    if (phase is null) continue;

                    sb.AppendLine($"[Phase] {phase.phaseId}");

                    if (phase.requireCenter && !SkillV2Util.IsValidTarget(request.Center))
                    {
                        sb.AppendLine("  [Phase] center invalid -> STOP");
                        return new SkillV2Report(false, sb.ToString());
                    }

                    // Select phase targets (can be empty if selector null or not found).
                    var phaseTargets = TargetSelectorExecutor.Select(phase.selector, request.Center, world.Map);

                    if (phase.requireTargets && phaseTargets.Count == 0)
                    {
                        sb.AppendLine("  [Phase] no targets -> STOP");
                        return new SkillV2Report(false, sb.ToString());
                    }

                    if (phase.stages is null || phase.stages.Count == 0)
                    {
                        sb.AppendLine("  [Phase] no stages -> skip");
                        continue;
                    }

                    for (int s = 0; s < phase.stages.Count; s++)
                    {
                        var stage = phase.stages[s];
                        if (stage is null) continue;

                        sb.AppendLine($"  [Stage] {stage.settleStage}");

                        // Filter invalid targets before stage.
                        FilterInvalid(phaseTargets);

                        if (stage.requireTargets && phaseTargets.Count == 0)
                        {
                            sb.AppendLine("    [Stage] no valid targets -> skip stage");
                            continue;
                        }

                        // Each stage has its own buffer; released once per stage.
                        var buffer = new EventBuffer(world.EventBus);

                        // Eval ctx shared across effects in this stage (CurrentTarget will be overwritten by BT foreach).
                        var eval = new SkillV2EvalContext(Config.skillId, CurrentCD, Host, request.Center, phaseTargets, null,
                            world.Map, world.EventBus, buffer, world.Rng);

                        if (stage.effects is null || stage.effects.Count == 0)
                        {
                            sb.AppendLine("    [Stage] no effects");
                        }
                        else
                        {
                            for (int e = 0; e < stage.effects.Count; e++)
                            {
                                var effect = stage.effects[e];
                                if (effect is null) continue;

                                sb.AppendLine($"    [Effect] {effect.effectId} desc={effect.description}");

                                var effectTargets = ResolveEffectTargets(effect, Host, request.Center, phaseTargets, world);
                                if (effect.requireTargets && effectTargets.Count == 0)
                                {
                                    sb.AppendLine("      [Effect] no targets -> skip");
                                    continue;
                                }

                                // Effect-level condition is evaluated once.
                                // Recommended: per-target conditions should be done inside BT (ForEachTarget + If predicate).
                                eval.CurrentTarget = PickConditionTarget(effectTargets, request.Center);
                                bool canRun = PredicateEvaluator.Eval(effect.condition, eval);

                                sb.AppendLine($"      [Effect] condition={canRun}");

                                if (!canRun) continue;

                                if (effect.behaviourTree is null)
                                {
                                    sb.AppendLine("      [Effect] behaviourTree is null -> FAIL");
                                    return new SkillV2Report(false, sb.ToString());
                                }

                                var bt = new BehaviourTreeRuntime(effect.behaviourTree, _actions);
                                var st = bt.Run(eval, effectTargets, effect.effectId, sb, indentDepth: 4);

                                sb.AppendLine($"      [Effect] BT result={st}");
                                if (st == NodeStatus.Failure)
                                {
                                    sb.AppendLine("      [Effect] BT failure -> STOP");
                                    return new SkillV2Report(false, sb.ToString());
                                }
                            }
                        }

                        // Stage release: changes become visible after this point.
                        sb.AppendLine($"    [Stage] bufferCount={buffer.Count} -> Release");
                        buffer.Release();

                        // Refresh targets for next stage if configured.
                        if (stage.refreshTargetsAfterRelease)
                        {
                            phaseTargets = TargetSelectorExecutor.Select(phase.selector, request.Center, world.Map);
                            sb.AppendLine($"    [Stage] refreshTargets -> count={phaseTargets.Count}");
                        }
                    }
                }

                CurrentCD = Config.cd;
                sb.AppendLine($"SkillV2 Finish: set CD={CurrentCD}");
                return new SkillV2Report(true, sb.ToString());
            }
            catch (Exception ex)
            {
                sb.AppendLine("Exception: " + ex);
                return new SkillV2Report(false, sb.ToString());
            }
        }

        private static List<Unit> ResolveEffectTargets(EffectDef effect, Unit host, Unit center, List<Unit> phaseTargets, SkillV2RuntimeContext world)
        {
            var res = new List<Unit>();
            if (effect is null) return res;

            switch (effect.targetSource)
            {
                case TargetSource.Self:
                    if (SkillV2Util.IsValidTarget(host)) res.Add(host);
                    return res;

                case TargetSource.Center:
                    if (SkillV2Util.IsValidTarget(center)) res.Add(center);
                    return res;

                case TargetSource.PhaseTargets:
                    if (phaseTargets != null) res.AddRange(phaseTargets);
                    FilterInvalid(res);
                    return res;

                case TargetSource.CustomSelector:
                    return TargetSelectorExecutor.Select(effect.customSelector, center, world.Map);

                default:
                    return res;
            }
        }

        private static Unit PickConditionTarget(List<Unit> effectTargets, Unit center)
        {
            if (effectTargets != null && effectTargets.Count > 0) return effectTargets[0];
            return center;
        }

        private static void FilterInvalid(List<Unit> targets)
        {
            if (targets is null) return;
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                if (!SkillV2Util.IsValidTarget(targets[i]))
                    targets.RemoveAt(i);
            }
        }
    }
}

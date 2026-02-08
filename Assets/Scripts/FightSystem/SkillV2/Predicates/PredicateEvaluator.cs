//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Refs;
using MyFrame.FightSystem.SkillV2.Configs;
using MyFrame.FightSystem.SkillV2.Expr;
using MyFrame.FightSystem.SkillV2.Refs;
using System;

namespace MyFrame.FightSystem.SkillV2.Predicates
{
    /// <summary>
/// Predicate evaluator for SkillV2.
/// 
/// Predicates are used in 3 places:
/// 1) Skill access condition (before execution)
/// 2) Effect condition (before running its BT)
/// 3) If node condition inside BehaviourTree
/// 
/// Predicates are defined in <see cref="Configs.PredicateDef"/> and <see cref="Configs.PredicateType"/>.
/// Most predicates read from current evaluation context:
/// - Host / Center / Target
/// - Skill runtime state (e.g. cooldown ready)
/// </summary>
    public static class PredicateEvaluator
    {
        public static bool Eval(PredicateDef def, SkillV2EvalContext ctx)
        {
            if (def is null) return true;

            switch (def.type)
            {
                case PredicateType.True:
                    return true;

                case PredicateType.Not:
                    return def.children is { Count: > 0 } && !Eval(def.children[0], ctx);

                case PredicateType.And:
                    if (def.children is null || def.children.Count == 0) return true;
                    foreach (var c in def.children)
                        if (!Eval(c, ctx)) return false;
                    return true;

                case PredicateType.Or:
                    if (def.children is null || def.children.Count == 0) return false;
                    foreach (var c in def.children)
                        if (Eval(c, ctx)) return true;
                    return false;

                case PredicateType.CompareExpr:
                    {
                        var l = ExprEvaluator.Eval(def.lhs, ctx);
                        var r = ExprEvaluator.Eval(def.rhs, ctx);
                        return Compare(l, r, def.op);
                    }

                case PredicateType.SkillCdReady:
                    return ctx.SkillCurrentCd == 0;

                case PredicateType.TargetHpRatioLe:
                    {
                        var t = ctx.CurrentTarget;
                        if (t is null) return false;
                        var max = (t.MaxHP <= 0f) ? t.HP : t.MaxHP;
                        var ratio = max <= 0f ? 0f : t.HP / max;
                        return ratio <= def.hpRatioThreshold;
                    }

                case PredicateType.TargetHasBuff:
                    return HasBuff(ctx.CurrentTarget, def.buffId);

                case PredicateType.RandomChance:
                    return (ctx.Rng?.NextDouble() ?? 0.0) <= (def.chance01 < 0f ? 0f : (def.chance01 > 1f ? 1f : def.chance01));

                default:
                    return true;
            }
        }

        private static bool Compare(float l, float r, CompareOp op) => op switch
        {
            CompareOp.LT => l < r,
            CompareOp.LE => l <= r,
            CompareOp.EQ => MathF.Abs(l - r) <= 0.0001f,
            CompareOp.NE => MathF.Abs(l - r) > 0.0001f,
            CompareOp.GT => l > r,
            CompareOp.GE => l >= r,
            _ => false
        };

        private static bool HasBuff(Unit u, string buffId)
        {
            if (u is null || string.IsNullOrWhiteSpace(buffId) || u.Buffs is null) return false;

            foreach (var b in u.Buffs)
            {
                if (b is null) continue;

                // Preferred: BuffBase exposes field BuffId.
                if (b is BuffBase bb && bb.BuffId == buffId) return true;

                // Fallback: reflection.
                var t = b.GetType();
                var f = t.GetField("BuffId");
                if (f != null && f.FieldType == typeof(string))
                {
                    var v = f.GetValue(b) as string;
                    if (v == buffId) return true;
                }
            }
            return false;
        }
    }
}

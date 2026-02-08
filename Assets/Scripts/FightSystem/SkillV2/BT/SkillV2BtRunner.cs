//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Refs;
using MyFrame.FightSystem.SkillV2.Actions;
using MyFrame.FightSystem.SkillV2.Configs;
using MyFrame.FightSystem.SkillV2.Predicates;
using MyFrame.FightSystem.SkillV2.Runtime;
using MyFrame.FightSystem.SkillV2.Refs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFrame.FightSystem.SkillV2.BT
{
    /// <summary>
/// BehaviourTree node evaluation result.
/// - Success: node completed successfully.
/// - Failure: node failed; parent control node decides how to continue.
/// </summary>
    public enum NodeStatus { Success, Failure }

    /// <summary>
/// Runtime interpreter for a table-driven BehaviourTree.
/// 
/// The tree is defined by <see cref="BehaviourTreeDef"/> (rootNodeId + nodes list).
/// Supported node types:
/// - And: execute children in order; any failure -> failure
/// - Or: execute children in order; any success -> success
/// - Not: invert child result
/// - If: evaluate predicate, then run then/else child
/// - ForEachTarget: iterate effectTargets, set eval.Target, run child per target
/// - Action: leaf node; dispatches to an action executor, usually buffering events into EventBuffer
/// </summary>
    public sealed class BehaviourTreeRuntime
    {
        private readonly Dictionary<string, BtNodeRow> _nodes;
        private readonly string _rootId;
        private readonly SkillActionRegistry _actions;

        public BehaviourTreeRuntime(BehaviourTreeDef def, SkillActionRegistry actions)
        {
            if (def is null) throw new ArgumentNullException(nameof(def));
            if (string.IsNullOrWhiteSpace(def.rootNodeId)) throw new ArgumentException("BT rootNodeId is empty");
            _rootId = def.rootNodeId;

            _nodes = new Dictionary<string, BtNodeRow>(StringComparer.Ordinal);
            if (def.nodes != null)
            {
                foreach (var n in def.nodes)
                {
                    if (n is null || string.IsNullOrWhiteSpace(n.nodeId)) continue;
                    _nodes[n.nodeId] = n;
                }
            }

            _actions = actions ?? throw new ArgumentNullException(nameof(actions));
        }

        /// <summary>
/// Execute the BT once.
/// 
/// Parameters:
/// - eval: evaluation context (host/center/world/eventBuffer etc.)
/// - effectTargets: targets resolved by EffectDef before entering BT
/// - effectId: used for logs/debug/tracing
/// - log: optional StringBuilder for trace
/// - indentDepth: visual indent for nested logs (Effect -> BT -> nodes)
/// </summary>
        public NodeStatus Run(SkillV2EvalContext eval, IReadOnlyList<Unit> effectTargets, string effectId, StringBuilder log, int indentDepth)
        {
            if (!_nodes.TryGetValue(_rootId, out var root))
            {
                log?.AppendLine($"{Indent(indentDepth)}[BT] root not found: {_rootId}");
                return NodeStatus.Failure;
            }

            return Tick(root, eval, effectTargets, effectId, log, indentDepth);
        }

        private NodeStatus Tick(BtNodeRow node, SkillV2EvalContext eval, IReadOnlyList<Unit> effectTargets, string effectId, StringBuilder log, int indentDepth)
        {
            if (node is null) return NodeStatus.Failure;

            switch (node.type)
            {
                case BtNodeType.And:
                    return TickAnd(node, eval, effectTargets, effectId, log, indentDepth);

                case BtNodeType.Or:
                    return TickOr(node, eval, effectTargets, effectId, log, indentDepth);

                case BtNodeType.Not:
                    return TickNot(node, eval, effectTargets, effectId, log, indentDepth);

                case BtNodeType.If:
                    return TickIf(node, eval, effectTargets, effectId, log, indentDepth);

                case BtNodeType.ForEachTarget:
                    return TickForEachTarget(node, eval, effectTargets, effectId, log, indentDepth);

                case BtNodeType.Action:
                    return TickAction(node, eval, effectId, log, indentDepth);

                default:
                    log?.AppendLine($"{Indent(indentDepth)}[BT] Unknown node type: {node.type} id={node.nodeId}");
                    return NodeStatus.Failure;
            }
        }

        private NodeStatus TickAnd(BtNodeRow node, SkillV2EvalContext eval, IReadOnlyList<Unit> effectTargets, string effectId, StringBuilder log, int indentDepth)
        {
            if (node.children is null || node.children.Count == 0) return NodeStatus.Success;

            foreach (var id in node.children)
            {
                if (!_nodes.TryGetValue(id, out var child))
                {
                    log?.AppendLine($"{Indent(indentDepth)}[BT.And] Missing child: {id}");
                    return NodeStatus.Failure;
                }

                var s = Tick(child, eval, effectTargets, effectId, log, indentDepth + 1);
                if (s == NodeStatus.Failure) return NodeStatus.Failure;
            }

            return NodeStatus.Success;
        }

        private NodeStatus TickOr(BtNodeRow node, SkillV2EvalContext eval, IReadOnlyList<Unit> effectTargets, string effectId, StringBuilder log, int indentDepth)
        {
            if (node.children is null || node.children.Count == 0) return NodeStatus.Failure;

            foreach (var id in node.children)
            {
                if (!_nodes.TryGetValue(id, out var child)) continue;
                var s = Tick(child, eval, effectTargets, effectId, log, indentDepth + 1);
                if (s == NodeStatus.Success) return NodeStatus.Success;
            }

            return NodeStatus.Failure;
        }

        private NodeStatus TickNot(BtNodeRow node, SkillV2EvalContext eval, IReadOnlyList<Unit> effectTargets, string effectId, StringBuilder log, int indentDepth)
        {
            if (node.children is null || node.children.Count == 0) return NodeStatus.Failure;
            var id = node.children[0];
            if (!_nodes.TryGetValue(id, out var child)) return NodeStatus.Failure;
            var s = Tick(child, eval, effectTargets, effectId, log, indentDepth + 1);
            return s == NodeStatus.Success ? NodeStatus.Failure : NodeStatus.Success;
        }

        private NodeStatus TickIf(BtNodeRow node, SkillV2EvalContext eval, IReadOnlyList<Unit> effectTargets, string effectId, StringBuilder log, int indentDepth)
        {
            bool cond = PredicateEvaluator.Eval(node.predicate, eval);
            log?.AppendLine($"{Indent(indentDepth)}[BT.If] id={node.nodeId} cond={cond}");

            // children[0] = then, children[1] = else
            if (node.children is null || node.children.Count == 0) return NodeStatus.Success;

            string branchId = cond ? node.children[0] : (node.children.Count > 1 ? node.children[1] : null);
            if (string.IsNullOrWhiteSpace(branchId)) return NodeStatus.Success;

            if (!_nodes.TryGetValue(branchId, out var child))
            {
                log?.AppendLine($"{Indent(indentDepth)}[BT.If] Missing branch node: {branchId}");
                return NodeStatus.Failure;
            }

            return Tick(child, eval, effectTargets, effectId, log, indentDepth + 1);
        }

        private NodeStatus TickForEachTarget(BtNodeRow node, SkillV2EvalContext eval, IReadOnlyList<Unit> effectTargets, string effectId, StringBuilder log, int indentDepth)
        {
            if (node.children is null || node.children.Count == 0) return NodeStatus.Success;
            if (effectTargets is null || effectTargets.Count == 0) return NodeStatus.Success;

            var childId = node.children[0];
            if (!_nodes.TryGetValue(childId, out var child))
            {
                log?.AppendLine($"{Indent(indentDepth)}[BT.ForEachTarget] Missing child: {childId}");
                return NodeStatus.Failure;
            }

            var prev = eval.CurrentTarget;

            for (int i = 0; i < effectTargets.Count; i++)
            {
                var t = effectTargets[i];
                if (!SkillV2Util.IsValidTarget(t))
                {
                    log?.AppendLine($"{Indent(indentDepth)}[BT.ForEachTarget] skip invalid target idx={i}");
                    continue;
                }

                eval.CurrentTarget = t;
                log?.AppendLine($"{Indent(indentDepth)}[BT.ForEachTarget] target={t.UnitId}");

                var s = Tick(child, eval, effectTargets, effectId, log, indentDepth + 1);
                if (s == NodeStatus.Failure)
                {
                    eval.CurrentTarget = prev;
                    return NodeStatus.Failure;
                }
            }

            eval.CurrentTarget = prev;
            return NodeStatus.Success;
        }

        private NodeStatus TickAction(BtNodeRow node, SkillV2EvalContext eval, string effectId, StringBuilder log, int indentDepth)
        {
            if (node.action is null)
            {
                log?.AppendLine($"{Indent(indentDepth)}[BT.Action] null action");
                return NodeStatus.Failure;
            }

            if (!_actions.TryGet(node.action.type, out var exec))
            {
                log?.AppendLine($"{Indent(indentDepth)}[BT.Action] no executor for type={node.action.type}");
                return NodeStatus.Failure;
            }

            var aCtx = new SkillV2ActionContext(eval, effectId, node.action.actionId);
            bool ok = exec.Execute(node.action, aCtx, out var msg);
            log?.AppendLine($"{Indent(indentDepth)}[BT.Action] id={node.nodeId} action={node.action.actionId} type={node.action.type} ok={ok} msg={msg}");

            return ok ? NodeStatus.Success : NodeStatus.Failure;
        }

        private static string Indent(int n) => n <= 0 ? "" : new string(' ', n * 2);
    }
}

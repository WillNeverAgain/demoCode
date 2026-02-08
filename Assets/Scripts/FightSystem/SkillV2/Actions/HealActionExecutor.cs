//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.EventSystem.Events;
using MyFrame.FightSystem.SkillV2.Configs;
using MyFrame.FightSystem.SkillV2.Expr;

namespace MyFrame.FightSystem.SkillV2.Actions
{
    /// <summary>
    /// Buffers a heal event for the current target. Does NOT directly change HP.
    /// </summary>
    public sealed class HealActionExecutor : ISkillActionExecutor
    {
        public ActionType Type => ActionType.Heal;

        public bool Execute(in ActionDef def, in SkillV2ActionContext ctx, out string message)
        {
            var target = ctx.Eval.CurrentTarget;
            if (target is null)
            {
                message = "HealAction: target is null";
                return false;
            }

            var amount = ExprEvaluator.Eval(def.amount, ctx.Eval);
            if (amount <= 0f)
            {
                message = $"HealAction: amount<=0 ({amount}) -> skipped";
                return true;
            }

            if (!ctx.Eval.Buffer.TryAdd(new SkillHealEventV2(ctx.Eval.Host, target, amount, ctx.Eval.SkillId, ctx.ActionId)))
            {
                message = "HealAction: failed to buffer SkillHealEventV2";
                return false;
            }

            message = $"HealAction: buffered SkillHealEventV2 host={ctx.Eval.Host?.UnitId} target={target.UnitId} heal={amount} action={ctx.ActionId}";
            return true;
        }
    }
}

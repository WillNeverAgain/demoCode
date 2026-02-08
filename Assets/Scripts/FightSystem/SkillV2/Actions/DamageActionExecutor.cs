//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.EventSystem.Events;
using MyFrame.FightSystem.SkillV2.Configs;
using MyFrame.FightSystem.SkillV2.Expr;

namespace MyFrame.FightSystem.SkillV2.Actions
{
    /// <summary>
    /// Buffers a damage event for the current target. Does NOT directly change HP.
    /// </summary>
    public sealed class DamageActionExecutor : ISkillActionExecutor
    {
        public ActionType Type => ActionType.Damage;

        public bool Execute(in ActionDef def, in SkillV2ActionContext ctx, out string message)
        {
            var target = ctx.Eval.CurrentTarget;
            if (target is null)
            {
                message = "DamageAction: target is null";
                return false;
            }

            var amount = ExprEvaluator.Eval(def.amount, ctx.Eval);
            if (amount <= 0f)
            {
                message = $"DamageAction: amount<=0 ({amount}) -> skipped";
                return true;
            }

            // Integrate with existing damage pipeline: publish SkillDamageEvent (legacy)
            if (!ctx.Eval.Buffer.TryAdd(new SkillDamageEvent(ctx.Eval.Host, target, amount, ctx.Eval.SkillId, ctx.ActionId)))
            {
                message = "DamageAction: failed to buffer SkillDamageEvent";
                return false;
            }

            message = $"DamageAction: buffered SkillDamageEvent host={ctx.Eval.Host?.UnitId} target={target.UnitId} dmg={amount} action={ctx.ActionId}";
            return true;
        }
    }
}

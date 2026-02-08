//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.EventSystem.Events;
using MyFrame.FightSystem.SkillV2.Configs;

namespace MyFrame.FightSystem.SkillV2.Actions
{
    /// <summary>
    /// Buffers a remove-buff event for the current target.
    /// </summary>
    public sealed class RemoveBuffActionExecutor : ISkillActionExecutor
    {
        public ActionType Type => ActionType.RemoveBuff;

        public bool Execute(in ActionDef def, in SkillV2ActionContext ctx, out string message)
        {
            var target = ctx.Eval.CurrentTarget;
            if (target is null)
            {
                message = "RemoveBuffAction: target is null";
                return false;
            }

            if (string.IsNullOrWhiteSpace(def.buffId))
            {
                message = "RemoveBuffAction: buffId is empty -> skipped";
                return true;
            }

            if (!ctx.Eval.Buffer.TryAdd(new SkillRemoveBuffEventV2(ctx.Eval.Host, target, def.buffId, 1, ctx.Eval.SkillId, ctx.ActionId)))
            {
                message = "RemoveBuffAction: failed to buffer SkillRemoveBuffEventV2";
                return false;
            }

            message = $"RemoveBuffAction: buffered SkillRemoveBuffEventV2 buff={def.buffId} target={target.UnitId}";
            return true;
        }
    }
}

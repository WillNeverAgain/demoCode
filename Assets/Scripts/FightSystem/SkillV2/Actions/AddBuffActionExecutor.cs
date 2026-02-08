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
    /// Buffers an add-buff event for the current target (or host depending on your pipeline).
    /// </summary>
    public sealed class AddBuffActionExecutor : ISkillActionExecutor
    {
        public ActionType Type => ActionType.AddBuff;

        public bool Execute(in ActionDef def, in SkillV2ActionContext ctx, out string message)
        {
            var target = ctx.Eval.CurrentTarget;
            if (target is null)
            {
                message = "AddBuffAction: target is null";
                return false;
            }

            if (string.IsNullOrWhiteSpace(def.buffId))
            {
                message = "AddBuffAction: buffId is empty -> skipped";
                return true;
            }

            if (!ctx.Eval.Buffer.TryAdd(new SkillAddBuffEventV2(ctx.Eval.Host, target, def.buffId, 1, ctx.Eval.SkillId, ctx.ActionId)))
            {
                message = "AddBuffAction: failed to buffer SkillAddBuffEventV2";
                return false;
            }

            message = $"AddBuffAction: buffered SkillAddBuffEventV2 buff={def.buffId} target={target.UnitId}";
            return true;
        }
    }
}

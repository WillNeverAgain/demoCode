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
    /// Buffers a move/teleport event for the current target (grid offset).
    /// </summary>
    public sealed class MoveActionExecutor : ISkillActionExecutor
    {
        public ActionType Type => ActionType.Move;

        public bool Execute(in ActionDef def, in SkillV2ActionContext ctx, out string message)
        {
            var target = ctx.Eval.CurrentTarget;
            if (target is null)
            {
                message = "MoveAction: target is null";
                return false;
            }

            if (def.moveDx == 0 && def.moveDy == 0)
            {
                message = "MoveAction: dx/dy=0 -> skipped";
                return true;
            }

            if (!ctx.Eval.Buffer.TryAdd(new SkillMoveEventV2(ctx.Eval.Host, target, def.moveDx, def.moveDy, ctx.Eval.SkillId, ctx.ActionId)))
            {
                message = "MoveAction: failed to buffer SkillMoveEventV2";
                return false;
            }

            message = $"MoveAction: buffered SkillMoveEventV2 target={target.UnitId} dx={def.moveDx} dy={def.moveDy}";
            return true;
        }
    }
}

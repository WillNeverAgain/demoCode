//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.SkillV2.Configs;

namespace MyFrame.FightSystem.SkillV2.Actions
{
    /// <summary>
/// Action executor interface (leaf runtime).
/// 
/// IMPORTANT:
/// - Executors should be pure with respect to game-state mutation, meaning they should only buffer events
///   into <see cref="SkillV2ActionContext.EventBuffer"/> (or equivalent) and NEVER directly modify Unit state.
/// - World state becomes visible to later stages only after EventBuffer.Release() at end of stage.
/// </summary>
    public interface ISkillActionExecutor
    {
        ActionType Type { get; }

        /// <summary>
        /// Execute action and write events to Eval.Buffer.
        /// </summary>
        bool Execute(in ActionDef def, in SkillV2ActionContext ctx, out string message);
    }
}

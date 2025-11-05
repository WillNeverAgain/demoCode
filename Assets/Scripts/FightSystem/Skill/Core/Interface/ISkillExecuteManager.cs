//Author : _SourceCode
//CreateTime : 2025-09-29-10:04:26
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Unit;

namespace MyFrame.FightSystem.Skill
{
    public interface ISkillExecuteManager
    {
        public void Execute(SkillExecuteCommand command);
    }
    public record SkillExecuteCommand(long skillID, in SkillContext skillContext, TargetPos aim) { }
}
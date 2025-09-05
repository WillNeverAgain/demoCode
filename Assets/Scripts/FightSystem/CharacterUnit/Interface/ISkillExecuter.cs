//Author : _SourceCode
//CreateTime : 2025-09-02-22:07:18
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill;
using MyFrame.FightSystem.Unit;

public interface ISkillExecuter : IFightObject
{
    public SkillReport ExecuteSkill(ISkill skill, SkillContext sctx, TargetPos aim);
}

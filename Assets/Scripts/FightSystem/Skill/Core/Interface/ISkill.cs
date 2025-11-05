//Author : _SourceCode
//CreateTime : 2025-08-20-19:34:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Unit;

namespace MyFrame.FightSystem.Skill
{
    public interface ISkill
    {
        public SkillReport Execute(in SkillContext sctx, TargetPos aim);
    }
}

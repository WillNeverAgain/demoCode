//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Interfaces
{
    public interface ISkillEffectDependency
    {
        bool Evaluate(EffectExecuteTrace context);
    }
}
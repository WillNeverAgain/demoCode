//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Interfaces
{
    public interface ISkillCondition
    {
        /// <summary>
        /// Checks whether the skill condition is met based on the provided context.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        ConditionResult Check(in SkillConditionContxt ctx);
    }
}


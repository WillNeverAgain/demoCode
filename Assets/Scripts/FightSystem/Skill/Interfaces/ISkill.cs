//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Refs;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill.Interfaces
{
    public interface ISkill
    {
        /// <summary>
        /// Checks whether the skill can be accessed based on the provided skill checks.
        /// </summary>
        /// <param name="ctx">Skill Context</param>
        /// <returns></returns>
        SkillCheckReport CheckAccess(in SkillConditionContxt ctx);
        // Executes the skill and returns a SkillReport.
        SkillReport Execute(in SkillExecuteContext ctx);
    }

    public interface ISkillEffect
    {
        EffectReport Execute(in SkillEffectContext ctx);
    }


}
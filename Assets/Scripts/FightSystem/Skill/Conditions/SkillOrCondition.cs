//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Refs;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill.Conditions
{
    public class SkillOrCondition : SkillConditionBase
    {
        public List<SkillConditionBase> conditions;
        public string failCodeIfAllFail = "OR_CONDITION_FAILED";
        public string messageKeyIfAllFail = "all_failed";
        protected override ConditionResult OnCheck(in SkillConditionContxt ctx)
        {
            foreach (var condition in conditions)
            {
                if (condition is null)
                {
                    continue;
                }
                var result = condition.Check(ctx);
                if (result.Success)
                {
                    return ConditionResult.Ok();
                }
            }
            return ConditionResult.Fail(failCodeIfAllFail,messageKeyIfAllFail);
        }
    }
}


//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Conditions
{
    public class SkillNotCondition : SkillConditionBase
    {
        public SkillConditionBase condition;
        protected override ConditionResult OnCheck(in SkillConditionContxt ctx)
        {
            var result = condition.Check(ctx);
            if (result.Success)
            {
                return ConditionResult.Fail(
                    code: "NOT_CONDITION_FAILED",
                    messageKey: "NotConditionFailed");
            }
            return ConditionResult.Ok();
        }
    }
}
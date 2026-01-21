//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Conditions
{
    public class SkillCDCondition : SkillConditionBase
    {
        protected override ConditionResult OnCheck(in SkillConditionContxt ctx)
        {
            return ctx.SkillRuntime.CurrentCD == 0
                ? ConditionResult.Ok()
                : ConditionResult.Fail(
                    code: "SKILL_ON_CD",
                    messageKey: "SkillIsOnCooldown",
                    ctx.SkillRuntime.CurrentCD);
        }
    }
}
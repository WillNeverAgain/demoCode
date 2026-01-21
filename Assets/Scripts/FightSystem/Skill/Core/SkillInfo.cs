//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Interfaces;

namespace MyFrame.FightSystem.Skill.Core
{
    public sealed class SkillInfo
    {
        public readonly string SkillId;
        public readonly string SkillName;
        public readonly string SkillDesc;

        public readonly uint CD;
        public readonly uint APCost;

        public readonly ISkillCondition Condition;

        public readonly ITargetSelector TargetSelector; 

        public readonly ISkillEffect Effect;

        public SkillInfo(string skillId, string skillName, string skillDesc, uint cD, uint aPCost, ISkillCondition condition, ITargetSelector targetSelector, ISkillEffect effect)
        {
            SkillId = skillId;
            SkillName = skillName;
            SkillDesc = skillDesc;
            CD = cD;
            APCost = aPCost;
            Condition = condition;
            TargetSelector = targetSelector;
            Effect = effect;
        }
    }
}
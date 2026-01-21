//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Effects
{
    public sealed class SkillEffectInfo
    {
        public readonly string Descreption;
        public readonly string EffectId;
        public readonly EffectTargetType TargetType;
        public readonly ISkillCondition Condition;
        public readonly ISkillEffectDependency Dependency;
        /// <summary>
        /// Next Effect Execution Flow Control
        /// </summary>
        public EffectStatus NextFlow = EffectStatus.Alays;

        public SkillEffectInfo(string descreption, string effectId, EffectTargetType targetType, ISkillCondition condition, ISkillEffectDependency dependency)
        {
            Descreption = descreption;
            EffectId = effectId;
            TargetType = targetType;
            Condition = condition;
            Dependency = dependency;
        }
    }



}
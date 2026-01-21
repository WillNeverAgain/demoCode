//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Effects
{
    /// <summary>
    /// Dependency: Negate the result of another dependency
    /// </summary>
    public sealed class DepNot : ISkillEffectDependency
    {
        private readonly ISkillEffectDependency _dep;
        public DepNot(ISkillEffectDependency dep) => _dep = dep;
        public bool Evaluate(EffectExecuteTrace ctx) => !(_dep?.Evaluate(ctx) ?? false);
    }



}
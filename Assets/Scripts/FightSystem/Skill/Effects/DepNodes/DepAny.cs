//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Effects
{
    /// <summary>
    /// Dependency: Any dependency is met
    /// </summary>
    public sealed class DepAny : ISkillEffectDependency
    {
        private readonly ISkillEffectDependency[] _deps;
        public DepAny(params ISkillEffectDependency[] deps) => _deps = deps;

        public bool Evaluate(EffectExecuteTrace ctx)
        {
            foreach (var d in _deps)
                if (d != null && d.Evaluate(ctx)) return true;
            return false;
        }
    }



}
//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Effects
{
    /// <summary>
    /// Dependency: Check if a certain flag/id is present in the EffectExecuteTrace
    /// </summary>
    public sealed class DepHas : ISkillEffectDependency
    {
        private readonly string _id;
        public DepHas(string id) => _id = id;
        public bool Evaluate(EffectExecuteTrace ctx) => ctx.Has(_id);
    }



}
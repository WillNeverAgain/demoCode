//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Effects
{
    /// <summary>
    /// Dependency: Check if the effect result with the given id has the expected status
    /// </summary>
    public sealed class DepStatusIs : ISkillEffectDependency
    {
        private readonly string _id;
        private readonly EffectStatus _expect;
        public DepStatusIs(string id, EffectStatus expect) { _id = id; _expect = expect; }

        public bool Evaluate(EffectExecuteTrace ctx)
            => ctx.TryGet(_id, out var r) && (r.Status & _expect)!=0;
    }



}
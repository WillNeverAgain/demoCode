//Author : _SourceCode
//CreateTime : 2025-09-08-11:25:20
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using JetBrains.Annotations;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Buff
{
    public class BuffDefinationBase : IBuff
    {
        public IBuffId Id { get; init; }

        public string Name { get; init; }

        public IReadOnlyList<IBuffTrigger> Triggers { get; init; }

        public IReadOnlyList<BuffTag> Tags { get; init; }
        public IBuffStackPolicy StackPolicy { get; init; }

        public IDuration Duration { get; init; }

        public IReadOnlyList<IBuffEffectNode> Effects { get; init; }
        public BuffDefinationBase(IBuffId id, string name, IReadOnlyList<IBuffTrigger> triggers, IReadOnlyList<BuffTag> tags, IBuffStackPolicy stackPolicy, IDuration duration, IReadOnlyList<IBuffEffectNode> effects)
        {
            Id = id;
            id.GenerateId(this);
            Name = name;
            Triggers = triggers;
            Tags = tags;
            StackPolicy = stackPolicy;
            Duration = duration;
            Effects = effects;
        }
    }
}

//Author : _SourceCode
//CreateTime : 2025-09-08-11:25:20
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

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
    }

    public class BuffInstance : IBuffInstance
    {
        public IBuff Buff { get; init; }

        public IBuffOwner Owner { get; init; }
#nullable enable
        public IBuffOwner? Source { get; init; }

        public int Stack { get; init; }
        public int? RemainingTime { get; init; }

        public bool IsExpired { get; init; }
    }
}

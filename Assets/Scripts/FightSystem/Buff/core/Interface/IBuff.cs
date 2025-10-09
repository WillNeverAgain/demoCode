//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System.Collections.Generic;

namespace MyFrame.FightSystem.Buff
{
    public interface IBuff
    {
        public IBuffId Id { get; }
        public string Name { get; }
        public IReadOnlyList<IBuffTrigger> Triggers { get; }
        public IReadOnlyList<BuffTag> Tags { get; }
        public IBuffStackPolicy StackPolicy { get; }
        public IDuration Duration { get; }
        public IReadOnlyList<IBuffEffectNode> Effects { get; }

    }
}
//Author : _SourceCode
//CreateTime : 2025-09-08-10:12:21
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using MyFrame.FightSystem.Event;
using MyFrame.FightSystem.Skill;

namespace MyFrame.FightSystem.Buff
{
    public class BuffContext
    {
        public IBuffOwner Owner { get; init; }
#nullable enable
        public IBuffOwner? Source { get; init; }
        public IEventBusCore EventBus { get; init; }
        public ValueCalculator ValueCalculator { get; init; }
        public IBlackBoard? BlackBoard { get; init; }

        public BuffContext(IBuffOwner owner,  IEventBusCore eventBus, ValueCalculator valueCalculator, IBuffOwner? source = null, IBlackBoard? blackBoard = null)
        {
            Owner = owner;
            Source = source;
            EventBus = eventBus;
            ValueCalculator = valueCalculator;
            BlackBoard = blackBoard;
        }
    }
}
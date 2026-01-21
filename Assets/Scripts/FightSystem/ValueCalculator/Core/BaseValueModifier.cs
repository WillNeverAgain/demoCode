//Author : _SourceCode
//CreateTime : 2026-01-14-12:45:36
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
using MyFrame.FightSystem.Calculator.Refs;
using System;
namespace MyFrame.FightSystem.Calculator.Core
{
    public abstract class BaseValueModifier : IComparable<BaseValueModifier>
    {
        public abstract int Priority { get;}
        public abstract ValueCalculateStage Stage { get; }
        public abstract string Message { get;}
        public abstract void Modify(ValueCalculateBreakPoint bd);

        public int CompareTo(BaseValueModifier other)
        {
            if(Stage < other.Stage) return -1;
            if(Stage > other.Stage) return 1;
            if(Priority < other.Priority) return -1;
            if(Priority > other.Priority) return 1;
            return 0;
        }

        
    }
}
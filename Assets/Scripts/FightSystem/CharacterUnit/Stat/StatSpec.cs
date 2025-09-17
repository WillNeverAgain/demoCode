//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;

namespace MyFrame.FightSystem.Unit
{
    public abstract class StatSpecBase
    {
        public StatType attributeType;
        public Type valueType;
        public StatFlag statFlag;
        public StatSpecBase(StatType attributeType, Type valueType,StatFlag statFlag)
        {
            this.attributeType = attributeType;
            this.valueType = valueType;
            this.statFlag = statFlag;
        }
    }
    public sealed class StatSpec<T> : StatSpecBase
    {
        public StatSpec(StatType attributeType,StatFlag statFlag) : base(attributeType, typeof(T),statFlag)
        {
            
        }
    }
    public enum StatFlag
    {
        None = 0,
        Modifiable = 1,
    }
}

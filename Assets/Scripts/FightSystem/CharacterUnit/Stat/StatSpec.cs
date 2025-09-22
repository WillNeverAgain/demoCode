//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;

namespace MyFrame.FightSystem.Unit
{
#nullable enable
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
        public bool Equals(StatSpecBase? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return attributeType == other.attributeType
                && valueType == other.valueType
                && statFlag == other.statFlag;
        }

        public override bool Equals(object? obj) => Equals(obj as StatSpecBase);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                hash = (hash ^ (int)attributeType) * 16777619;
                hash = (hash ^ valueType.GetHashCode()) * 16777619;
                hash = (hash ^ (int)statFlag) * 16777619;
                return hash;
            }
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

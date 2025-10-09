//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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

    public static class SpecTrans
    {
        private static readonly Dictionary<StatType, (Type valueType, StatFlag defaultFlag)> _dictionary = new()
        {
            { StatType.name,             (typeof(string), StatFlag.None) },
            { StatType.description,      (typeof(string), StatFlag.None) },
            { StatType.attribute,        (typeof(Element), StatFlag.None) },

            { StatType.hp_max,           (typeof(int),    StatFlag.Modifiable) },
            { StatType.hp,               (typeof(int),    StatFlag.Modifiable) },
            { StatType.p_atk,            (typeof(int),    StatFlag.Modifiable) },
            { StatType.p_def,            (typeof(int),    StatFlag.Modifiable) },
            { StatType.m_atk,            (typeof(int),    StatFlag.Modifiable) },
            { StatType.m_def,            (typeof(int),    StatFlag.Modifiable) },

            { StatType.critical_rate,    (typeof(float),  StatFlag.Modifiable) },
            { StatType.act_order_point,  (typeof(int),  StatFlag.Modifiable) },
            { StatType.move_point,       (typeof(int),  StatFlag.Modifiable) },
            { StatType.act_point_main,   (typeof(int),  StatFlag.Modifiable) },
            { StatType.act_point_assisdent,(typeof(int),StatFlag.Modifiable) },
        };
        public static StatSpecBase Trans(StatType stat)
        {
            if (!_dictionary.TryGetValue(stat, out var entry))
                throw new StatSpecTransFailedException(nameof(stat), $"No mapping for {stat}");

            var valueType = entry.valueType;
            var flag = entry.defaultFlag;
            var closed = typeof(StatSpec<>).MakeGenericType(valueType);
            return (StatSpecBase)Activator.CreateInstance(closed, stat, flag)!;
        }
        public static bool TryTrans<T>(StatType stat,out StatSpec<T> spec)
        {
            var specBase = Trans(stat);
            if(specBase.valueType == typeof(T))
            {
                spec = (StatSpec<T> )(specBase);
                return true;    
            }
            else
            {
                spec = null!;
                return false;
            }
        }
        public static bool TryAs<T>(StatSpecBase specBase,out StatSpec<T> spec)
        {
            if (specBase.valueType == typeof(T))
            {
                spec = (StatSpec<T>)(specBase);
                return true;
            }
            else
            {
                spec = null!;
                return false;
            }
        }
    
    }
#nullable disable
    [Serializable]
    public class StatSpecTransFailedException : Exception
    {
        private string v1;
        private string v2;

        public StatSpecTransFailedException()
        {
        }

        public StatSpecTransFailedException(string message) : base(message)
        {
        }

        public StatSpecTransFailedException(string v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public StatSpecTransFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StatSpecTransFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

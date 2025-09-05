//Author : _SourceCode
//CreateTime : 2025-08-13-02:24:49
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem;
using MyFrame.FightSystem.Calculator;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

public class BaseDamageCalculator : IBaseDamageCalculator
{
    private const float MIN_ATTACK_RATE = 0.01F;
    public float Compute(ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> unit_attribute, 
        ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> target_attribute, 
        AttackType attack_type)
    {
        float attack_value;
        float defense_value;
        switch (attack_type)
        {
            case AttackType.Physics:
                attack_value = unit_attribute[AttributeType.p_atk].Value;
                defense_value = target_attribute[AttributeType.p_def].Value;
                break;
            case AttackType.Magic:
                attack_value = unit_attribute[AttributeType.m_atk].Value;
                defense_value = target_attribute[AttributeType.m_def].Value;
                break;
            default:
                throw new AttackTypeException("wrong attacktype,there must be something wrong with your code");
        }
        return MathF.Max(attack_value - defense_value, attack_value * MIN_ATTACK_RATE);
    }
}

[Serializable]
internal class AttackTypeException : Exception
{
    public AttackTypeException()
    {
    }

    public AttackTypeException(string message) : base(message)
    {
    }

    public AttackTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected AttackTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
//Author : _SourceCode
//CreateTime : 2025-08-13-02:24:49
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyFrame.FightSystem.Skill
{
    public class BaseDamageCalculator : IBaseDamageCalculator
    {
        private readonly float MIN_ATTACK_RATE;
        private List<IBaseDamageModPolicy> _baseDamageModPolicy;
        public ValueBreakdown Compute(BaseDamageConfig config)
        {
            float attack_value;
            float defense_value;
            switch (config.AttackType)
            {
                case AttackType.Physics:
                    attack_value = config.SkillExecuter.GetAttributeValue(StatType.p_atk).Value;
                    defense_value = config.SkillTarget.GetAttributeValue(StatType.p_def).Value;
                    break;
                case AttackType.Magic:
                    attack_value = config.SkillExecuter.GetAttributeValue(StatType.m_atk).Value;
                    defense_value = config.SkillTarget.GetAttributeValue(StatType.m_def).Value;
                    break;
                default:
                    throw new AttackTypeException("wrong attacktype,there must be something wrong with your code");
            }
            ValueBreakdown bd = new ValueBreakdown(MathF.Max(attack_value - defense_value, attack_value * MIN_ATTACK_RATE));
            var bctx = new BaseDamageModContext(config.SkillExecuter.GetAttributeValue(StatType.critical_rate).Value, config.DamageRate);

            foreach(var modifier in _baseDamageModPolicy)
            {
                bd = modifier.Modify(bd,bctx);
            }
            return bd;

        }
        public BaseDamageCalculator(float mIN_ATTACK_RATE, List<IBaseDamageModPolicy> baseDamageModPolicy)
        {
            MIN_ATTACK_RATE = mIN_ATTACK_RATE;
            _baseDamageModPolicy = baseDamageModPolicy;
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
}
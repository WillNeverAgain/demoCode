//Author : _SourceCode
//CreateTime : 2025-08-13-02:24:49
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using MyFrame.FightSystem.Unit;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyFrame.FightSystem.Skill
{
    public sealed class BaseDamageCalculator : IBaseDamageCalculator
    {
        public float MinAttackRate { get; }
        private readonly IReadOnlyList<IBaseDamageModPolicy> _policies;

        // 预先缓存常用 StatSpec，避免每帧创建/反射
        private static readonly StatSpec<int> Spec_PAtk;
        private static readonly StatSpec<int> Spec_PDef;
        private static readonly StatSpec<int> Spec_MAtk;
        private static readonly StatSpec<int> Spec_MDef;
        private static readonly StatSpec<float> Spec_CritRate;

        // AttackType -> (atkSpec, defSpec) 映射，避免 switch 重复
        private static readonly Dictionary<AttackType, (StatSpec<int> atk, StatSpec<int> def)> Map_IntPair;

        static BaseDamageCalculator()
        {
            // 若映射配置和期望类型不一致，会在启动时就抛出，便于尽早发现问题
            if (!SpecTrans.TryTrans<int>(StatType.p_atk, out Spec_PAtk))
                throw new StatSpecTransFailedException("p_atk 应为 int");
            if (!SpecTrans.TryTrans<int>(StatType.p_def, out Spec_PDef))
                throw new StatSpecTransFailedException("p_def 应为 int");
            if (!SpecTrans.TryTrans<int>(StatType.m_atk, out Spec_MAtk))
                throw new StatSpecTransFailedException("m_atk 应为 int");
            if (!SpecTrans.TryTrans<int>(StatType.m_def, out Spec_MDef))
                throw new StatSpecTransFailedException("m_def 应为 int");
            if (!SpecTrans.TryTrans<float>(StatType.critical_rate, out Spec_CritRate))
                throw new StatSpecTransFailedException("critical_rate 应为 float");

            Map_IntPair = new()
            {
                { AttackType.Physics, (Spec_PAtk, Spec_PDef) },
                { AttackType.Magic,   (Spec_MAtk, Spec_MDef) },
            };
        }

        public BaseDamageCalculator(float minAttackRate, IReadOnlyList<IBaseDamageModPolicy> baseDamageModPolicy)
        {
            if (minAttackRate < 0f || minAttackRate > 1f)
                throw new ArgumentOutOfRangeException(nameof(minAttackRate), "minAttackRate 应在 [0,1]");

            MinAttackRate = minAttackRate;
            _policies = baseDamageModPolicy ?? Array.Empty<IBaseDamageModPolicy>();
        }

        public ValueBreakdown Compute(BaseDamageConfig config)
        {
            if (config is null) throw new ArgumentNullException(nameof(config));
            if (config.SkillExecuter is null) throw new ArgumentNullException(nameof(config.SkillExecuter));
            if (config.SkillTarget is null) throw new ArgumentNullException(nameof(config.SkillTarget));

            if (!Map_IntPair.TryGetValue(config.AttackType, out var pair))
                throw new AttackTypeException($"未支持的 AttackType: {config.AttackType}");

            // 取攻防数值（整型）
            int attackValue = config.SkillExecuter.GetStat(pair.atk)._result;
            int defenseValue = config.SkillTarget.GetStat(pair.def)._result;

            // 基础伤害下限：攻击 * MinAttackRate
            float attackF = attackValue;
            float baseDamage = MathF.Max(attackF - defenseValue, attackF * MinAttackRate);

            // 暴击率与技能倍率进入修饰上下文
            float critRate = config.SkillExecuter.GetStat(Spec_CritRate)._result;
            var bctx = new BaseDamageModContext(critRate, config.DamageRate);

            // 应用修饰策略链
            var bd = new ValueBreakdown(baseDamage);
            for (int i = 0; i < _policies.Count; i++)
            {
                bd = _policies[i].Modify(bd, bctx);
            }
            return bd;
        }
    }

    [Serializable]
    internal class AttackTypeException : Exception
    {
        public AttackTypeException() { }
        public AttackTypeException(string message) : base(message) { }
        public AttackTypeException(string message, Exception innerException) : base(message, innerException) { }
        protected AttackTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

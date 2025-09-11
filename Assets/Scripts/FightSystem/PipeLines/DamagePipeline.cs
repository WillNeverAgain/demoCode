//Author : _SourceCode
//CreateTime : 2025-09-10-15:46:51
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;

namespace MyFrame.FightSystem.Pipeline
{
    public class DamagePipeline : IDamagePipeline
    {
        private IValueCalculator _valueCalculator;
        private IBaseDamageCalculator _baseDamageCalculator;
        public ValueBreakdown Damage(DamageConfigs configs)
        {
            BaseDamageConfig base_config = new BaseDamageConfig(configs.attackType, configs.skillExecuter, configs.skillTarget,configs.damageRate);
            ValueBreakdown bd = _baseDamageCalculator.Compute(base_config);

            return bd;
        }
    }
}

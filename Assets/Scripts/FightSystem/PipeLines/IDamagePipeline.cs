//Author : _SourceCode
//CreateTime : 2025-09-10-15:46:51
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;

namespace MyFrame.FightSystem.Pipeline
{
    public interface IDamagePipeline
    {
        public ValueBreakdown Damage(DamageConfigs configs);
    }
}

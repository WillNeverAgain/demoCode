//Author : _SourceCode
//CreateTime : 2025-08-12-01:21:36
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


using System.Collections.Generic;

namespace MyFrame.FightSystem.Calculator
{
    public interface IDamageCalculator
    {
        public DamageBreakdown compute(in DamageContext ctx , IEnumerable<IDamageModifier> all_mods);
    }
}

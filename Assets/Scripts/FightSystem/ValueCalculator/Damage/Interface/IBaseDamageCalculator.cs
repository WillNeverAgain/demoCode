//Author : _SourceCode
//CreateTime : 2025-08-13-02:25:44
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem;
using MyFrame.FightSystem.Calculator;
using System.Collections.ObjectModel;

public interface IBaseDamageCalculator
{
    public ValueBreakdown Compute(BaseDamageConfig config);
}

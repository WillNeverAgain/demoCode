//Author : _SourceCode
//CreateTime : 2026-01-14-12:45:36
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
using System.Collections.Generic;
using MyFrame.FightSystem.Calculator.Refs;
using MyFrame.FightSystem.Calculator.Interfaces;
namespace MyFrame.FightSystem.Calculator.Core
{
    public class ValueCalculator : IValuCalculator
    {
        public ValueReport Calculate(float value, IEnumerable<BaseValueModifier> modifiers)
        {
            ValueCalculateBreakPoint bd = new()
            {
                CurrentValue = value,
                Message = $"BaseValueֵ:{value}"
            };

            List<BaseValueModifier> sortedModifiers = new List<BaseValueModifier>(modifiers);
            sortedModifiers.Sort();

            foreach (var modifier in sortedModifiers)
            {
                modifier.Modify(bd);
            }

            return new ValueReport(value, bd.CurrentValue, bd.Message);
        }
    }
}
//Author : _SourceCode
//CreateTime : 2025-08-25-17:53:28
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyFrame.FightSystem.Unit
{
    public class UnitAttributeCalculator : IAttributeCalculator<StatContext>
    {
        private readonly IValueCalculator _valueCalculator;
        private readonly int MAX_CALCULATOR_TIMES = 20;

        public UnitAttributeCalculator(IValueCalculator valueCalculator)
        {
            _valueCalculator = valueCalculator;
        }

        public ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> GetValue(StatContext ctx , IAttributeData<StatContext> data)
        {
            int count = 0;
            while (count < MAX_CALCULATOR_TIMES && data.GetDirty(out List<AttributeDataUnit<StatContext> > next_attributes))
            {
                if (next_attributes == null || next_attributes.Count == 0) { break; }
                foreach(AttributeDataUnit<StatContext> next_attribute in next_attributes)
                {
                    ValueBreakdown bd = _valueCalculator.Compute(ctx, next_attribute.ValueModifiers, next_attribute.Base);
                    data.SetValue(next_attribute.Type, bd.Result);
                    count++;
                }
                ctx.UnitAttribute = data.GetValue();
            }
            return data.GetValue();
        }
    }

}
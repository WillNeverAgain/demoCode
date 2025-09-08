//Author : _SourceCode
//CreateTime : 2025-08-25-17:44:17
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.ObjectModel;

namespace MyFrame.FightSystem.Unit
{
    public class UnitAttribute : IAttribute<StatContext>
    {
        private readonly IAttributeCalculator<StatContext> _attributeCalculator;
        private readonly IAttributeData<StatContext> _data;

        private ReadOnlyDictionary<AttributeType,AttributeDataUnit<StatContext>> _last;

        public UnitAttribute(IAttributeCalculator<StatContext> attributeCalculator, IAttributeData<StatContext> data, ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> last)
        {
            _attributeCalculator = attributeCalculator;
            _data = data;
            _last = last;
        }

        public void AddModifier(AttributeType type,IValueModifier<StatContext> valueModifier)
        {
            _data.AddModifier(type, valueModifier);
        }

        /// <summary>
        /// 获取值，同时更新属性表，传入的ctx不需要包含属性表
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> GetValue(StatContext ctx)
        {
            ctx.UnitAttribute = _attributeCalculator.GetValue(ctx , _data);
            if ( !CalculateDirty(ctx)) { return _last; }

            //重计算
            _last = _attributeCalculator.GetValue(ctx , _data);

            return _last;
        }

        public void RemoveModifier(AttributeType type,IValueModifier<StatContext> valueModifier)
        {
            _data.RemoveModifier(type, valueModifier);
        }

        public void SetValue(AttributeType type, float value)
        {
            _data.SetValue(type, value);
        }

        /// <summary>
        /// 计算脏标记，决定是否重算
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        private bool CalculateDirty(StatContext ctx)
        {
            return true;
        }


    }
}

//Author : _SourceCode
//CreateTime : 2025-08-12-03:37:10
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Calculator
{
    public class StatContext : IValueContext
    {
        public float BaseValue
        {
            get;
        }

        public StatValueType ValueType
        {
            get;
        }

        public StatContext(float baseValue, StatValueType valueType)
        {
            this.BaseValue = baseValue;
            this.ValueType = valueType;
        }
    }
}
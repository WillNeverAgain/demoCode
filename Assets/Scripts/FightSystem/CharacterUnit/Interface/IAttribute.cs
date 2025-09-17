//Author : _SourceCode
//CreateTime : 2025-08-25-15:22:15
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.ObjectModel;
using Unity.VisualScripting;

namespace MyFrame.FightSystem.Unit
{
    public interface IAttribute<T> where T : IValueContext
    {
        public ReadOnlyDictionary<StatType, AttributeDataUnit<T>> GetValue(T ctx);
        public void AddModifier(StatType type,IValueModifier<T> valueModifier);
        public void RemoveModifier(StatType type, IValueModifier<T> valueModifier);
        public void SetValue(StatType type , float value);
    }

}
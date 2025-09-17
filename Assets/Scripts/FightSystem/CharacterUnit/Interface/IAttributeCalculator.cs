//Author : _SourceCode
//CreateTime : 2025-08-24-14:55:10
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.ObjectModel;

public interface IAttributeCalculator<T> where T : IValueContext
{
    public ReadOnlyDictionary<StatType, AttributeDataUnit<T>> GetValue(T ctx , IAttributeData<T> data);
}

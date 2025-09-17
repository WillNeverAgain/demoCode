//Author : _SourceCode
//CreateTime : 2025-08-24-14:56:07
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public interface IAttributeData<T> where T : IValueContext
{
    public ReadOnlyDictionary<StatType, AttributeDataUnit<T>> GetValue();
    public bool SetValue(StatType type , float value);
    public bool AddModifier(StatType type, IValueModifier<T> modifier);
    public bool RemoveModifier(StatType type, IValueModifier<T> modifier);
    public bool GetDirty(out List<AttributeDataUnit<T>> next_attributes);
}

public class AttributeDataUnit<T> where T : IValueContext
{
    public StatType Type { get; }
    public float Base {  get; }
    public float Value { get; set; }
    public List<string> Note {  get; set; }
    public List<IValueModifier<T>> ValueModifiers { get; set; }
    public AttributeDataUnit(StatType _type,float _base,float _value,List<string> _Note,List<IValueModifier<T>> _ValueModifiers)
    {
        Type = _type;
        Base = _base;
        Value = _value;
        Note = _Note;
        ValueModifiers = _ValueModifiers;
    }

    public AttributeDataUnit<T> Clone()
    {
        return new AttributeDataUnit<T>(Type, Base, Value, Note, ValueModifiers);
    }
}

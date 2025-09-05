//Author : _SourceCode
//CreateTime : 2025-08-25-17:53:45
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class UnitAttributeData : IAttributeData<StatContext>
{
    private readonly Dictionary<AttributeType, AttributeDataUnit<StatContext>> _data;
    private bool _dirty = false;
    public bool AddModifier(AttributeType type, IValueModifier<StatContext> modifier)
    {
        if (!_data.ContainsKey(type)) {  return false; }
        _data[type].ValueModifiers.Add(modifier);
        _dirty = true;
        return true;
    }

    public bool RemoveModifier(AttributeType type, IValueModifier<StatContext> modifier)
    {
        if(!_data.ContainsKey(type)) { return false; }
        if (!_data[type].ValueModifiers.Contains(modifier)) { return false; }
        _data[type].ValueModifiers.Remove(modifier);
        _dirty = true;
        return true;
    }


    public ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> GetValue()
    {
        return new ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>>(_data);
    }



    public bool SetValue(AttributeType type, float value, List<string> report)
    {
        if (!_data.ContainsKey(type)) { return false;}
        if(value != _data[type].Value) {_dirty = true;}
        _data[type].Value = value;
        return true;
    }

    public bool GetDirty(out List<AttributeDataUnit<StatContext>> next_attributes)
    {
        next_attributes = new List<AttributeDataUnit<StatContext>>();
        if (!_dirty) { return false ; }

        foreach (AttributeType attributeDataUnit in _data.Keys)
        {
            next_attributes.Add(_data[attributeDataUnit].Clone());
        }
        return true;
    }

    public UnitAttributeData(Dictionary<AttributeType, AttributeDataUnit<StatContext>> data)
    {
        if(data == null) { data = new Dictionary<AttributeType, AttributeDataUnit<StatContext>>(); }
        _data = data;
        _dirty = false;
    }
}

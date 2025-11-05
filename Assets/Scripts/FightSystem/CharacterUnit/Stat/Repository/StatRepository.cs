//Author : _SourceCode
//CreateTime : 2025-09-18-08:08:00
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System;
using System.Collections.Generic;
#nullable enable
namespace MyFrame.FightSystem.Unit
{
    public class StatRepository : IStatRepository
    {
        public long Version { get; private set; } = 0;
        private readonly Dictionary<StatSpecBase,object?> _base = new();
        private readonly Dictionary<StatSpecBase, List<IStatModifier>> _modifiers = new();

        public void AddModifier(IStatModifier modifier)
        {
            if(modifier == null) return;
            if(_modifiers.TryGetValue(modifier.Spec,out var list))
            {
                list.Add(modifier);
                return;
            }
            var new_list = new List<IStatModifier>();
            new_list.Add(modifier);
            _modifiers.Add(modifier.Spec, new_list);
            Version++;
        }

        public IReadOnlyList<IStatModifier> EnumerateModifiers(StatSpecBase target)
        {
            if(target == null) return Array.Empty<IStatModifier>();
            if(!_modifiers.TryGetValue(target,out var list)) return Array.Empty<IStatModifier>();
            if(list == null || list.Count == 0) return Array.Empty<IStatModifier>();
            return list.ToArray();
        }

        public T ReadBase<T>(StatSpec<T> stat_spec)
        {
            if(stat_spec == null) return default(T)!;
            if(!_base.TryGetValue(stat_spec,out var value)) return default(T)!;
            return (T)value!;
        }

        public bool RemoveModifier(IStatModifier modifier)
        {
            if(!_modifiers.TryGetValue(modifier.Spec, out var list)) return false;
            if(list == null || !list.Contains(modifier)) return false;
            list.Remove(modifier);
            return true;
        }

        public void SetBase<T>(StatSpec<T> stat_spec, T value)
        {
            if(stat_spec == null || value == null) return;
            if(!_base.TryAdd(stat_spec, value)) _base[stat_spec] = value;
            Version++;
            return;
        }

        public bool TryReadBase<T>(StatSpec<T> stat_spec, out T value)
        {
            value = default(T)!;
            if(stat_spec == null) return false;
            if(!_base.ContainsKey(stat_spec)) return false;
            value = (T)_base[stat_spec]!; 
            return true;
        }
    }
}

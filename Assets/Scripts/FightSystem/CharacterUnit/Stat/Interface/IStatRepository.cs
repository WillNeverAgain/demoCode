//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Unit
{
    public interface IStatRepository
    {
        public long Version { get; }
        public void AddModifier(IStatModifier modifier);
        public bool RemoveModifier(IStatModifier modifier);
        public IReadOnlyList<IStatModifier> EnumerateModifiers(StatSpecBase target);
        public void SetBase<T>(StatSpec<T> stat_spec, T value);
        public T ReadBase<T>(StatSpec<T> stat_spec);
        public bool TryReadBase<T>(StatSpec<T> stat_spec, out T value);
    }

}

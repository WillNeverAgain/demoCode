//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;

namespace MyFrame.FightSystem.Unit
{
    public interface IStatRepository
    {
        public void AddModifier(IStatModifier modifier);
        public void RemoveModifier(IStatModifier modifier);
        public T Set<T>(StatSpec<T> stat_spec, T value);
        public T Get<T>(StatSpec<T> stat_spec, StatContext ctx);
        public bool TryGet<T>(StatSpec<T> stat_spec, StatContext ctx, out T value);
    }

}

//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;

namespace MyFrame.FightSystem.Unit
{
    public interface IStatCache : IReadOnlyStatCache
    {
        public void Set<T>(StatSpec<T> spec, StatContext ctx, long version, T value);
    }
    public interface IReadOnlyStatCache
    {
        public bool TryGet<T>(StatContext ctx, StatSpecBase statSpec, long version, out T value);
    }

}

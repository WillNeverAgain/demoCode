//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace MyFrame.FightSystem.Unit
{
    public interface IStatManagerChildNode : IReadOnlyStatManagerChildNode
    {
        public void AddModifier(IStatModifier modifier);
        public bool RemoveModifier(IStatModifier modifier);
        public void SetBase<T>(StatSpec<T> stat_spec, T value);
    }
    public interface IReadOnlyStatManagerChildNode
    {
        public StatResult<T> Get<T>(StatSpec<T> stat_spec, StatContext ctx);
        public bool TryGet<T>(StatSpec<T> stat_spec, StatContext ctx, out StatResult<T> value);
    }

}

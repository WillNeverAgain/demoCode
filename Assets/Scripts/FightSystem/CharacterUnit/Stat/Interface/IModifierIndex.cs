//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Unit
{
    public interface IModifierIndex
    {
        public IEnumerable<IStatModifier> Query(StatContext ctx,StatSpecBase statSpec,out long version);
        public bool HasAny(StatContext ctx,StatSpecBase statSpec,out long version);
    }
}
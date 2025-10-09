//Author : _SourceCode
//CreateTime : 2025-09-18-10:08:58
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Unit
{
    public class ModifierIndexOneChche : IModifierIndex
    {
        private IStatRepository _repo;
        public bool HasAny(StatContext ctx, StatSpecBase statSpec, out long version)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IStatModifier> Query(StatContext ctx, StatSpecBase statSpec, out long version)
        {
            throw new System.NotImplementedException();
        }
    }
}


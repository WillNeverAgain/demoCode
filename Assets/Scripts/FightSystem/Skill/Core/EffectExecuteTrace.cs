//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
using System.Collections.Generic;
using System.Linq;

namespace MyFrame.FightSystem.Skill.Refs
{
    public sealed class EffectExecuteTrace
    {
        private readonly Dictionary<string, EffectResult> _results = new();
        public int Depth { get; set; } = 0;

        public void Record(string id, EffectResult res) => _results[id] = res;

        public bool TryGet(string id, out EffectResult res) => _results.TryGetValue(id, out res);

        public bool Has(string id) => _results.ContainsKey(id);
    }
}
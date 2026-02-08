//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;
using System.Collections.Generic;

namespace MyFrame.FightSystem.SkillV2.Configs
{
    /// <summary>
    /// One behaviour tree node record (table row).
    /// </summary>
    [Serializable]
    public sealed class BtNodeRow
    {
        public string nodeId;
        public BtNodeType type;

        /// <summary>Children node ids. And/Or: N children. Not: 1 child. If: 2 children (then, else).</summary>
        public List<string> children = new();

        public PredicateDef predicate; // If node uses it.

        public ActionDef action; // Action node uses it.
    }
}

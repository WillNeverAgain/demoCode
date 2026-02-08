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
    /// BehaviourTree definition for an effect (root id + node list).
    /// </summary>
    [Serializable]
    public sealed class BehaviourTreeDef
    {
        public string rootNodeId;
        public List<BtNodeRow> nodes = new();
    }
}

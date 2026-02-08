//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;

namespace MyFrame.FightSystem.SkillV2.Configs
{
    /// <summary>
    /// The minimal judgment unit of a skill. Selects targets, checks condition, then runs its BehaviourTree.
    /// </summary>
    [Serializable]
    public sealed class EffectDef
    {
        public string effectId = "effect_xxx";
        public string description = "";

        public TargetSource targetSource = TargetSource.PhaseTargets;
        public TargetSelectorDef customSelector;

        /// <summary>Effect requires at least one target after resolving TargetSource + filtering.</summary>
        public bool requireTargets = true;

        public PredicateDef condition;

        public BehaviourTreeDef behaviourTree;
    }
}

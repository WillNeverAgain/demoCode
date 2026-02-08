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
    /// One execution phase of a skill. Contains ordered settle stages and a default target selector.
    /// </summary>
    [Serializable]
    public sealed class PhaseDef
    {
        public SkillPhaseId phaseId = SkillPhaseId.Impact;

        /// <summary>Enter phase requires a valid center.</summary>
        public bool requireCenter = true;

        /// <summary>Enter phase requires at least one selected target (after filtering).</summary>
        public bool requireTargets = true;

        /// <summary>Phase target selection config.</summary>
        public TargetSelectorDef selector;

        public List<StageDef> stages = new();
    }
}

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
    /// A settle stage inside a phase. Buffers events for all effects, then releases them in batch.
    /// </summary>
    [Serializable]
    public sealed class StageDef
    {
        public SkillSettleStage settleStage = SkillSettleStage.Value;

        /// <summary>Enter stage requires at least one valid phase target.</summary>
        public bool requireTargets = true;

        /// <summary>
        /// After this stage releases buffered events, should we refresh phase targets by re-selecting?
        /// Default true: allows next stage to see state changes (dead/moved targets etc.).
        /// </summary>
        public bool refreshTargetsAfterRelease = true;

        public List<EffectDef> effects = new();
    }
}

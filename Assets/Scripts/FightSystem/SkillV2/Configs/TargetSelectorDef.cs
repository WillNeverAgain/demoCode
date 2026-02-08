//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;

namespace MyFrame.FightSystem.SkillV2.Configs
{
    /// <summary>
    /// Target selector definition used to compute target sets.
    /// </summary>
    [Serializable]
    public sealed class TargetSelectorDef
    {
        public TargetSelectorType type = TargetSelectorType.AoeSquare;

        /// <summary>radius for AOE selectors.</summary>
        public int radius = 1;

        /// <summary>Include center unit itself in selected targets.</summary>
        public bool includeCenter = false;

        /// <summary>Optional: only include units matching this bitmask (0 means no filter).</summary>
        public int effectTargetMask = 0;
    }
}

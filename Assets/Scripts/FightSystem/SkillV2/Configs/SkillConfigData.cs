//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;
using System.Collections.Generic;

namespace MyFrame.FightSystem.SkillV2.Configs
{
    /// <summary>
    /// Pure data skill config (table-driven).
    /// 
    /// IMPORTANT (design invariants):
    /// 1) Runtime MUST NOT directly mutate world state from Skill/Phase/Stage. All mutations must go through:
    ///    Effect -> BehaviourTree -> Action(leaf) -> EventBuffer -> Release.
    /// 2) This type is JSON friendly. It is the recommended runtime config container.
    /// 3) Excel tables should be exported into this JSON structure (see Tools/SkillV2TableExporter).
    /// </summary>
    [Serializable]
    public sealed class SkillConfigData
    {
        /// <summary>Unique skill id, used as lookup key.</summary>
        public string skillId = "skill_xxx";

        /// <summary>Display name (UI).</summary>
        public string skillName = "New Skill";

        /// <summary>Description (UI).</summary>
        public string skillDesc = "";

        /// <summary>Cooldown</summary>
        public uint cd = 0;

        /// <summary>AP cost.</summary>
        public uint apCost = 0;

        /// <summary>
        /// Skill access condition, evaluated before execution.
        /// Typical: SkillCdReady, HasBuff, etc.
        /// Null means always allowed.
        /// </summary>
        public PredicateDef? accessCondition;

        /// <summary>
        /// Phase list in execution order.
        /// Runtime executes: phases[0] -> phases[1] -> ...
        /// </summary>
        public List<PhaseDef> phases = new();
    }
}

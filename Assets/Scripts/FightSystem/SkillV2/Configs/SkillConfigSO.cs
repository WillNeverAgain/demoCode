//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace MyFrame.FightSystem.SkillV2.Configs
{
    [CreateAssetMenu(menuName = "Fight/SkillV2/SkillConfig", fileName = "SkillConfigV2")]
    /// <summary>
/// Unity ScriptableObject wrapper for SkillV2 configs.
/// 
/// Recommended production pipeline is: Excel -> JSON -> runtime load <see cref="SkillConfigData"/>.
/// This SO is kept for editor-side quick authoring/debugging only.
/// </summary>
    public sealed class SkillConfigSO : ScriptableObject
    {
        public string skillId = "skill_xxx";
        public string skillName = "New Skill";
        [TextArea] public string skillDesc = "";

        public uint cd = 0;
        public uint apCost = 0;

        /// <summary>
        /// Skill level access condition (table-driven). Typical: SkillCdReady.
        /// </summary>
        public PredicateDef accessCondition;

        public List<PhaseDef> phases = new();
    }
}

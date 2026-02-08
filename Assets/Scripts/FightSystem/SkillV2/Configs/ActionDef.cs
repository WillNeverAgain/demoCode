//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;

namespace MyFrame.FightSystem.SkillV2.Configs
{
    /// <summary>
    /// Action definition (leaf operation). Executed by an action executor; should buffer events only.
    /// </summary>
    [Serializable]
    public sealed class ActionDef
    {
        public string actionId = "action_xxx";
        public ActionType type = ActionType.Damage;

        public ExprDef amount;

        /// <summary>buff id (Add/Remove).</summary>
        public string buffId;

        /// <summary>Move offset in grid.</summary>
        public int moveDx;
        public int moveDy;
    }
}

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
    /// Predicate definition (table-driven condition) used by skill/effect/if nodes.
    /// </summary>
    [Serializable]
    public sealed class PredicateDef
    {
        public PredicateType type = PredicateType.True;

        public List<PredicateDef> children = new();

        // CompareExpr
        public ExprDef lhs;
        public ExprDef rhs;
        public CompareOp op = CompareOp.LE;

        // TargetHpRatioLe
        public float hpRatioThreshold = 0.3f;

        // HasBuff
        public string buffId;

        // RandomChance
        public float chance01 = 0.5f;
    }
}

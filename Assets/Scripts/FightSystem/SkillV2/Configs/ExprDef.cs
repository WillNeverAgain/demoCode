//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System;

namespace MyFrame.FightSystem.SkillV2.Configs
{
    /// <summary>
    /// Expression definition used by actions/predicates to compute numeric values.
    /// </summary>
    [Serializable]
    public sealed class ExprDef
    {
        public ExprType type = ExprType.Constant;

        public float constant = 0f;

        public ExprDef a;
        public ExprDef b;

        public float min = 0f;
        public float max = 0f;
    }
}

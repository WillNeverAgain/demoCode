//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
namespace MyFrame.FightSystem.SkillV2.Configs
{
    public enum SkillPhaseId
    {
        PreCast = 0,
        Cast = 10,
        Impact = 20,
        AfterCast = 30,
    }

    /// <summary>
    /// Within one Phase, stages run in order. Each stage buffers events, then releases them in batch.
    /// After release, world state changes become visible to the next stage's condition evaluation.
    /// </summary>
    public enum SkillSettleStage
    {
        Pre = 0,
        Value = 10,
        Damage = 20,
        PostDamage = 30,
        Buff = 40,
        Move = 50,
        Cleanup = 90,
    }

    public enum TargetSource
    {
        Self = 0,
        Center = 1,
        PhaseTargets = 2,
        CustomSelector = 3,
    }

    public enum TargetSelectorType
    {
        None = 0,
        /// <summary>Square (Chebyshev distance) around center's cell position.</summary>
        AoeSquare = 10,
        /// <summary>Diamond (Manhattan distance) around center's cell position.</summary>
        AoeDiamond = 20,
    }

    public enum BtNodeType
    {
        And = 0,
        Or = 10,
        Not = 20,
        If = 30,
        ForEachTarget = 40,
        Action = 90,
    }

    public enum ActionType
    {
        Damage = 0,
        Heal = 10,
        AddBuff = 20,
        RemoveBuff = 30,
        Move = 40,
        PlayVfx = 90,
    }

    public enum PredicateType
    {
        True = 0,
        Not = 10,
        And = 20,
        Or = 30,

        /// <summary>Compare two expressions.</summary>
        CompareExpr = 40,

        /// <summary>Skill's current CD is ready (==0).</summary>
        SkillCdReady = 50,

        /// <summary>Target HP ratio <= threshold.</summary>
        TargetHpRatioLe = 60,

        /// <summary>Target has buffId.</summary>
        TargetHasBuff = 70,

        /// <summary>Random chance success.</summary>
        RandomChance = 80,
    }

    public enum CompareOp
    {
        LT, LE, EQ, NE, GT, GE
    }

    public enum ExprType
    {
        Constant = 0,

        HostAttack = 10,
        HostDefence = 11,
        HostHP = 12,

        TargetAttack = 20,
        TargetDefence = 21,
        TargetHP = 22,

        Add = 30,
        Mul = 31,
        Min = 32,
        Max = 33,
        Clamp = 34,
    }
}

//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.SkillV2.Configs;
using MyFrame.FightSystem.SkillV2.Refs;
using System;

namespace MyFrame.FightSystem.SkillV2.Expr
{
    /// <summary>
/// Expression evaluator for SkillV2.
/// 
/// It evaluates <see cref="Configs.ExprDef"/> into float values, using data from:
/// - Host (caster) stats
/// - Target stats (current iterated target in BT/Effect)
/// 
/// Supported nodes are defined in <see cref="Configs.ExprType"/>:
/// Constant, HostAttack/Defence/HP, TargetAttack/Defence/HP, Add/Mul/Min/Max/Clamp.
/// </summary>
    public static class ExprEvaluator
    {
        public static float Eval(ExprDef def, SkillV2EvalContext ctx)
        {
            if (def is null) return 0f;

            switch (def.type)
            {
                case ExprType.Constant: return def.constant;

                case ExprType.HostAttack: return ctx.Host?.Attack ?? 0f;
                case ExprType.HostDefence: return ctx.Host?.Defence ?? 0f;
                case ExprType.HostHP: return ctx.Host?.HP ?? 0f;

                case ExprType.TargetAttack: return ctx.CurrentTarget?.Attack ?? 0f;
                case ExprType.TargetDefence: return ctx.CurrentTarget?.Defence ?? 0f;
                case ExprType.TargetHP: return ctx.CurrentTarget?.HP ?? 0f;

                case ExprType.Add: return Eval(def.a, ctx) + Eval(def.b, ctx);
                case ExprType.Mul: return Eval(def.a, ctx) * Eval(def.b, ctx);
                case ExprType.Min: return MathF.Min(Eval(def.a, ctx), Eval(def.b, ctx));
                case ExprType.Max: return MathF.Max(Eval(def.a, ctx), Eval(def.b, ctx));
                case ExprType.Clamp:
                    {
                        var v = Eval(def.a, ctx);
                        return MathF.Min(def.max, MathF.Max(def.min, v));
                    }

                default: return 0f;
            }
        }
    }
}

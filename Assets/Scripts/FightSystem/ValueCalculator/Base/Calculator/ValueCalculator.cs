//Author : _SourceCode
//CreateTime : 2025-08-12-01:21:49
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Calculator
{
    public class ValueCalculator : IValueCalculator
    {
        /// <summary>
        /// 计算最终伤害
        /// </summary>
        /// <param name="ctx">数据上下文，包含影响数据的因素</param>
        /// <param name="all_mods">调整器容器，包含所有应当应用的规则</param>
        /// <returns>计算结果容器，包含初始值，最终值以及溯源记录</returns>
        public ValueBreakdown Compute<T, U>(in T ctx, IEnumerable<U> all_mods , ValueBreakdown bd)
            where T : IValueContext
            where U : IValueModifier<T>
        {
            float value = bd.Result;

            value = applyStage(ctx, all_mods, value, ModifierStage.PreAdd,ref bd);
            value = applyStage(ctx, all_mods, value, ModifierStage.Multiplier,ref bd);
            value = applyStage(ctx, all_mods, value, ModifierStage.PostAdd,ref bd);
            value = applyStage(ctx, all_mods, value, ModifierStage.Finalize,ref bd);

            value = MathF.Ceiling(value);

            bd.Result = value;

            return bd;
        }

        private float applyStage<T, U>(in T ctx , IEnumerable<U> all_mods ,float current_value , ModifierStage current_stage,ref ValueBreakdown bd) 
            where T : IValueContext
            where U : IValueModifier<T>
        {
            float addSum = 0f;
            float mulProd = 1f;
            float cur = current_value;

            foreach (var m in all_mods)
            {
                if (m.Stage != current_stage || !m.ApplyTo(ctx)) continue;

                switch (m.Stacking)
                {
                    case StageStacking.Additive:
                        var add = m.GetValue(ctx);
                        addSum += add;
                        bd.Notes.Add($"+{add} 来自 {m.Source}");
                        break;

                    case StageStacking.Multiplicative:
                        var mul = m.GetValue(ctx);
                        mulProd *= mul;
                        bd.Notes.Add($"×{mul:0.###} 来自 {m.Source}");
                        break;

                    case StageStacking.Override:
                        cur = m.GetValue(ctx);
                        bd.Notes.Add($"=覆写为 {cur} 来自 {m.Source}");
                        break;

                    case StageStacking.ClampMin:
                        var minV = m.GetValue(ctx);
                        cur = MathF.Max(cur, minV);
                        bd.Notes.Add($"≥{minV} 最低保障 来自 {m.Source}");
                        break;

                    case StageStacking.ClampMax:
                        var maxV = m.GetValue(ctx);
                        cur = MathF.Min(cur, maxV);
                        bd.Notes.Add($"≤{maxV} 上限限制 来自 {m.Source}");
                        break;
                }
            }

            cur = (cur + addSum) * mulProd;
            return cur;
        }
    }
}



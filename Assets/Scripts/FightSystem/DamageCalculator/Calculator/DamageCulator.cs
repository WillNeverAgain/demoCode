//Author : _SourceCode
//CreateTime : 2025-08-12-01:21:49
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Calculator
{
    public class DamageCulator : IDamageCalculator
    {
        public const float MIN_DAMAGE_RATE = 0.01F;
        /// <summary>
        /// 计算最终伤害
        /// </summary>
        /// <param name="ctx">伤害上下文，包含攻击者，防御者，地形等因素</param>
        /// <param name="all_mods">调整器容器，包含所有应当应用的规则</param>
        /// <returns>计算结果容器，包含初始值，最终值以及溯源记录</returns>
        public DamageBreakdown compute(in DamageContext ctx, IEnumerable<IDamageModifier> all_mods)
        {

            //Max( 进攻者实战攻击力 - 防守者实战防御力 , 进攻者实战攻击力 * 最小伤害比例 ) * 伤害倍率 * 属性克制系数 * 暴击伤害修正（没暴击就是1）

            float value = MathF.Max(ctx.EffectiveAttack - ctx.EffectiveDefense, ctx.EffectiveAttack * MIN_DAMAGE_RATE);
            DamageBreakdown db_res = new DamageBreakdown(value);

            value = applyStage(ctx, all_mods, value, ModifierStage.PreAdd,db_res);
            value = applyStage(ctx, all_mods, value, ModifierStage.Multiplier, db_res);
            value = applyStage(ctx, all_mods, value, ModifierStage.PostAdd, db_res);
            value = applyStage(ctx, all_mods, value, ModifierStage.Finalize, db_res);

            value = MathF.Floor(value);

            db_res.Result = value;

            return db_res;
        }


        private float applyStage(in DamageContext ctx , IEnumerable<IDamageModifier> all_mods ,float current_value , ModifierStage current_stage, DamageBreakdown bd)
        {
            float addSum = 0f;
            float mulProd = 1f;
            float cur = current_value;

            foreach (var m in all_mods)
            {
                if (m.Stage != current_stage || !m.applyTo(ctx)) continue;

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

                    //case StageStacking.Override:
                    //    cur = m.GetValue(ctx);
                    //    bd.Notes.Add($"=覆写为 {cur} 来自 {m.Source}");
                    //    break;

                    //case StageStacking.ClampMin:
                    //    var minV = m.GetValue(ctx);
                    //    cur = MathF.Max(cur, minV);
                    //    bd.Notes.Add($"≥{minV} 最低保障 来自 {m.Source}");
                    //    break;

                    //case StageStacking.ClampMax:
                    //    var maxV = m.GetValue(ctx);
                    //    cur = MathF.Min(cur, maxV);
                    //    bd.Notes.Add($"≤{maxV} 上限限制 来自 {m.Source}");
                    //    break;
                }
            }

            cur = (cur + addSum) * mulProd;
            return cur;
        }
    }
}



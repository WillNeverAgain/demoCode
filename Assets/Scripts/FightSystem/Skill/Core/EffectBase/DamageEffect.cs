//Author : _SourceCode
//CreateTime : 2025-08-22-16:18:57
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyFrame.FightSystem.Skill
{
    public class DamageEffect : IEffect
    {
        private IValueCalculator _valueCalculator;
        private IBaseDamageCalculator _baseDamageCalculator;
        private float damage_rate;
        private AttackType attack_type;
        public EffectReport Execute(SkillContext sctx, IBlackBoard blackBoard, ISkillTarget t)
        {
            EffectReport effectReport = new EffectReport();
            ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> unit_attribute = sctx.unit.GetAttributeValue();
            ReadOnlyDictionary < AttributeType, AttributeDataUnit < StatContext >> target_attribute = t.GetAttributeValue();
            ValueBreakdown bd_atk = _valueCalculator.Compute(new DamageContext(sctx.unit, t),sctx.unit.GetModifiers<DamageContext>(),_baseDamageCalculator.Compute(unit_attribute,target_attribute,attack_type));
            ValueBreakdown bd_final = _valueCalculator.Compute(new DefenseContext(), t.GetModifiers<DefenseContext>(), bd_atk.Result);

            effectReport.Notes.AddRange(bd_atk.Notes);
            effectReport.Notes.AddRange(bd_final.Notes);
            return effectReport;
        }
    }
}

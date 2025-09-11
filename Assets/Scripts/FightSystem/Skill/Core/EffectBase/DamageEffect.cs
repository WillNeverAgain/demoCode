//Author : _SourceCode
//CreateTime : 2025-08-22-16:18:57
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using MyFrame.FightSystem.Pipeline;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyFrame.FightSystem.Skill
{
    public class DamageEffect : IEffect
    {
        private IDamagePipeline _damagePipeline;
        private float damage_rate;
        private AttackType attack_type;
        public EffectReport Execute(SkillContext sctx, IBlackBoard blackBoard, ISkillTarget t)
        {
            EffectReport effectReport = new EffectReport();
            DamageConfigs damageConfigs = new(sctx.unit,t,attack_type,damage_rate);
            _damagePipeline.Damage(damageConfigs);
            return effectReport;
        }
    }
}

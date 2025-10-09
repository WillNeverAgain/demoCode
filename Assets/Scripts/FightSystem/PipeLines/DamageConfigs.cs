//Author : _SourceCode
//CreateTime : 2025-09-10-15:46:51
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using MyFrame.FightSystem.Skill;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Pipeline
{
    public class DamageConfigs
    {
        public ISkillExecuter skillExecuter;
        public ISkillTarget skillTarget;
        public AttackType attackType;
        public float damageRate;
        public DamageConfigs(ISkillExecuter skillExecuter,ISkillTarget skillTarget,AttackType attackType,float damageRate)
        {
            this.skillExecuter = skillExecuter;
            this.skillTarget = skillTarget;
            this.attackType = attackType;
            this.damageRate = damageRate;
        }
    }
}
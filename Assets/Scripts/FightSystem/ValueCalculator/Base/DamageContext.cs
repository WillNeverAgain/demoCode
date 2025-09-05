//Author : _SourceCode
//CreateTime : 2025-08-12-01:23:12
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


using MyFrame.FightSystem.Skill;
using MyFrame.FightSystem.Unit;

namespace MyFrame.FightSystem.Calculator
{
    public class DamageContext : IValueContext
    {
        public ISkillExecuter Attacker
        {
            get;
        }

        public ISkillTarget Defenser
        {
            get;
        }

        public DamageContext(ISkillExecuter attacker, ISkillTarget defenser)
        {
            Attacker = attacker;
            Defenser = defenser;
        }
    }

}
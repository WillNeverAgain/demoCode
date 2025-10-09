//Author : _SourceCode
//CreateTime : 2025-09-11-08:07:18
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System;

namespace MyFrame.FightSystem.Skill
{
    public class BaseDamageModCriticalPolicy : IBaseDamageModPolicy
    {
        public ValueBreakdown Modify(ValueBreakdown bd, BaseDamageModContext ctx)
        {
            Random random = new Random();
            int r = random.Next(0, 1000);
            if ((int)ctx.critical_rate * 1000 >= r)
            {
                bd.Result = bd.Result * ctx.critical_damage_rate;
                bd.Notes.Add("mod from cristical_hit :" + ctx.critical_damage_rate);
            }
            return bd;
        }
    }
}

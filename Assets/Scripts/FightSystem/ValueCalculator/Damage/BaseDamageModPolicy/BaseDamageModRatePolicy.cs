//Author : _SourceCode
//CreateTime : 2025-09-11-08:07:18
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;

namespace MyFrame.FightSystem.Skill
{
    public class BaseDamageModRatePolicy : IBaseDamageModPolicy
    {
        public ValueBreakdown Modify(ValueBreakdown bd, BaseDamageModContext ctx)
        {
            bd.Result = bd.Result * ctx.damage_rate;
            bd.Notes.Add("damage rate: "+ ctx.damage_rate);
            return bd;
        }
    }
}

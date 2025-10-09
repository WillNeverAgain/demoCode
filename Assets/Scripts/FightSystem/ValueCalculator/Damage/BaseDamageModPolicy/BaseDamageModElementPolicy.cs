//Author : _SourceCode
//CreateTime : 2025-09-11-08:07:18
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill
{
    public class BaseDamageModElementPolicy : IBaseDamageModPolicy
    {
        private static float restrain_rate = 1.3f;
        private static float restrained_rate = 0.7f;
        private Dictionary<Element, List<Element>> map = new() {
            {Element.Physical,new List<Element>{ Element.Antipsionic } },
            {Element.Psionic,new List<Element>{ Element.Physical } },
            {Element.Antipsionic,new List<Element>{ Element.Psionic } },
            {Element.Holy,new List<Element>{ Element.Profane } },

        };
        public ValueBreakdown Modify(ValueBreakdown bd, BaseDamageModContext ctx)
        {
            float mod_rate = SearchMap(ctx.attacker_element, ctx.defender_element);
            bd.Result = bd.Result * mod_rate;
            bd.Notes.Add("mod from Element restrain : " +  mod_rate);
            return bd;
        }
        private float SearchMap(Element attacker,Element defender)
        {
            if (map.TryGetValue(attacker, out var atk_map) && atk_map.Contains(defender)) return restrain_rate;
            if (map.TryGetValue(defender, out var def_map) && def_map.Contains(attacker)) return restrained_rate;
            return 1f;
        }
    }
}

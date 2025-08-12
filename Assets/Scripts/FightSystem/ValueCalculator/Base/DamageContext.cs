//Author : _SourceCode
//CreateTime : 2025-08-12-01:23:12
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


namespace MyFrame.FightSystem.Calculator
{
    public class DamageContext : IValueContext
    {
        public FightUnit Attacker
        {
            get;
        }

        public FightUnit Defenser
        {
            get;
        }

        public float BaseValue
        {
            get;
        }
    }

}
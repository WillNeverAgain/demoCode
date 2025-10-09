//Author : _SourceCode
//CreateTime : 2025-08-13-02:24:49
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Skill
{
    public class BaseDamageModContext
    {
        public float critical_rate { get; }
        public float critical_damage_rate {  get; }
        public float damage_rate { get; }
        public Element attacker_element {  get; }
        public Element defender_element { get; }
        public BaseDamageModContext(float critical_rate, float damage_rate)
        {
            this.critical_rate = critical_rate;
            this.damage_rate = damage_rate;
        }
    }
}
//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.EventSystem.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.EventSystem.Events
{
    public sealed class SkillDamageEvent : IEvent
    {
        public Unit Host;
        public Unit Target;
        public float DamageAmount;
        public string SkillId;
        public string RuntimeId;

        public SkillDamageEvent(Unit host, Unit target, float damageAmount, string skillId, string runtimeId)
        {
            Host = host;
            Target = target;
            DamageAmount = damageAmount;
            SkillId = skillId;
            RuntimeId = runtimeId;
        }
    }
}
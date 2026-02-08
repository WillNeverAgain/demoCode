//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.SkillV2.Runtime
{
    public static class SkillV2Util
    {
        /// <summary>
        /// Define "target exists" here. For now: not null and HP > 0.
        /// Extend if you have "Removed/Invincible/Unselectable" flags.
        /// </summary>
        public static bool IsValidTarget(Unit u) => u is not null && u.HP > 0f;
    }
}

//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Core;

namespace MyFrame.FightSystem.Skill.Refs
{
    public readonly struct SkillConditionContxt
    { 
        public SkillRuntime SkillRuntime { get;  }
        public SkillConditionContxt(SkillRuntime skillRuntime)
        {
            SkillRuntime = skillRuntime;
        }
    }
}
//Author : _SourceCode
//CreateTime : 2025-08-20-19:34:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill
{
    public class EffectRule
    {
        public List<FightObjectType> TargetTypes { get; private set; }
        public IEffect Effect { get; private set; }
        public List<IEffectApplyRule> ApplyRule { get; private set; }
    }
}

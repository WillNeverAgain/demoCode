//Author : _SourceCode
//CreateTime : 2025-08-20-19:35:04
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill
{
    public interface ISkillTargetSelector
    {
        public List<ISkillTarget> Select(SelectorQuery selectorQuery, IWordQuery wordQuery);    
    }
}

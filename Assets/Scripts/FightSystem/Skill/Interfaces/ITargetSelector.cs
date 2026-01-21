//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using System.Collections.Generic;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Interfaces
{
    public interface ITargetSelector
    {
        IEnumerable<Unit> Select(Unit center,Unit host);
    }


}
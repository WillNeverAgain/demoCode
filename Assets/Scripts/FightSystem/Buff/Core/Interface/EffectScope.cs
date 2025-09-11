//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Event;
using MyFrame.FightSystem.Skill;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Buff
{
    public class EffectScope
    {
        public IBuffInstance Buff { get; }
#nullable enable
        public IFightSceneEvent? TriggerEvent { get; }
        public int TriggerCount { get; internal set; }
        public IBlackBoard? BlackBoard { get; }

        public EffectScope(IBuffInstance buff, IFightSceneEvent? evt = null, int count = 0)
        {
            Buff = buff; TriggerEvent = evt; TriggerCount = count;
        }
    }
}
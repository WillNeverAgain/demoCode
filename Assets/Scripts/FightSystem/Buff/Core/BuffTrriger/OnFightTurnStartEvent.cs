//Author : _SourceCode
//CreateTime : 2025-09-10-13:02:32
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Buff
{
    public class OnFightTurnStartEvent : IFightSceneEvent
    {
        public string Name { get; } = "OnFightTurnStartEvent";
        public string Source { get; init; }
        public OnFightTurnStartEvent(string source)
        {
            Source = source;
        }
    }
}
//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Buff
{
    public class BuffApplyArgs
    {
        public BuffContext Ctx { get; init; }
        public IBuffOwner Source { get;init; }
        public int AddStacks { get; init; } = 1;
    }
}
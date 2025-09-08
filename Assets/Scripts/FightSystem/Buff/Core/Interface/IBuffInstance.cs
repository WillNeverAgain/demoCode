//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Buff
{
    public interface IBuffInstance
    {
        public IBuff Buff { get; }
        public IBuffOwner Owner { get; }
        public IBuffOwner Source { get; }
        public int Stack {  get; }
        public int? RemainingTime {  get; }
        public bool IsExpired {  get; }

    }
}
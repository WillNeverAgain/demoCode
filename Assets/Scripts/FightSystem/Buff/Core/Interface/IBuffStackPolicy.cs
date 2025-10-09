//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Buff
{
    public interface IBuffStackPolicy
    {
        public BuffStackOp Decide(IBuffInstance instance, BuffApplyArgs args);
        public void Apply(IBuffInstance instance, BuffApplyArgs args);
    }
}
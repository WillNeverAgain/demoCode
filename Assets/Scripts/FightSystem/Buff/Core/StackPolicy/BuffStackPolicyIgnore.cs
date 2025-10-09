//Author : _SourceCode
//CreateTime : 2025-09-10-14:39:46
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Buff
{
    public class BuffStackPolicyIgnore : IBuffStackPolicy
    {
        public void Apply(IBuffInstance instance, BuffApplyArgs args)
        {
            return;
        }

        public BuffStackOp Decide(IBuffInstance instance, BuffApplyArgs args)
        {
            return BuffStackOp.Ignore;
        }
    }
}


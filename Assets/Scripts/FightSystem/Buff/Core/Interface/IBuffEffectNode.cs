//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Buff
{
    public interface IBuffEffectNode
    {
        public BuffEffectReport Excute(BuffContext ctx,EffectScope scope);
        public BuffEffectReport OnEnter(BuffContext ctx);
        public BuffEffectReport OnExit(BuffContext ctx);
    }
}
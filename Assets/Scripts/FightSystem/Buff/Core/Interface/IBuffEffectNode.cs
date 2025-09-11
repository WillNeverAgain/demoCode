//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Buff
{
    public interface IBuffEffectNode
    {
        public void Excute(BuffContext ctx,EffectScope scope);
        public void OnEnter(BuffContext ctx);
        public void OnExit(BuffContext ctx);
    }
}
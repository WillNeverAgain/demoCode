//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Buff
{
    public interface IDuration
    {
        public bool OnTick(IBuffInstance instance,BuffContext ctx, int dt);
        public void OnRefresh(IBuffInstance instance, BuffContext ctx, int? override_time);
        public void OnStackChanged(IBuffInstance instance , BuffContext ctx);
        public int? GetTime(IBuffInstance instance);
    }
}
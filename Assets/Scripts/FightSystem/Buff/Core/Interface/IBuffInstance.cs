//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Event;

namespace MyFrame.FightSystem.Buff
{
    public interface IBuffInstance
    {
        public IBuff Buff { get; }
        public IBuffOwner Owner { get; }
        public IBuffOwner Source { get; }
        public int Stack {  get; }
        public bool IsExpired {  get; }
        public int TriggerCount {  get; }

        public void OnAttached(BuffContext ctx);
        public void OnTick(BuffContext ctx,int dt);
        public void OnRemoved(BuffContext ctx,BuffRemoveReason reason);
        public void OnEvent(BuffContext ctx,IFightSceneEvent evt);


        public void SetStacks(BuffContext ctx,int delta);
        public void AddStack(BuffContext ctx,int stack);
        public void Refresh(BuffContext ctx, int? override_time = null);

    }
}
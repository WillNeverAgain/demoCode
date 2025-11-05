//Author : _SourceCode
//CreateTime : 2025-09-10-13:02:32
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Event;
using System;

namespace MyFrame.FightSystem.Buff
{
    public class BuffTriggerOnTurn : IBuffTrigger
    {
        private IEventBusCore _EventBusCore;
        private IDisposable disposable;
        public void Bind(IBuffInstance buffInstance, BuffContext ctx)
        {
            disposable = _EventBusCore.Subscribe<OnFightTurnStartEvent>(e =>
            {
                buffInstance.OnEvent(ctx ,e);
            });
        }

        public void Unbind(IBuffInstance buffInstance, BuffContext ctx)
        {
            if (disposable == null) return;
            disposable.Dispose();
        }
    }
}

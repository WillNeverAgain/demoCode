//Author : _SourceCode
//CreateTime : 2025-09-08-11:25:20
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;

namespace MyFrame.FightSystem.Buff
{
    public class BuffInstance : IBuffInstance
    {
        public IBuff Buff { get; }

        public IBuffOwner Owner { get; }
#nullable enable
        public IBuffOwner? Source { get; }

        public int Stack { get; private set; }

        public bool IsExpired { get; private set; }

        public int TriggerCount { get; private set; }

        public void AddStack(BuffContext ctx, int stack)
        {
            if(IsExpired || stack == 0) return;
            Stack = Math.Max(Stack+stack, 0);
            Buff.Duration.OnStackChanged(this,ctx);
        }

        public void OnAttached(BuffContext ctx)
        {
            foreach(var effect in Buff.Effects)
            {
                effect.OnEnter(ctx);
            }
            foreach(var trigger  in Buff.Triggers)
            {
                trigger.Bind(this, ctx);
            }
            Owner.OnBuffAttached(this);
        }
        /// <summary>
        /// 事件驱动，buff效果仅在此处触发
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="evt"></param>
        public void OnEvent(BuffContext ctx, IFightSceneEvent evt)
        {
            if(IsExpired) return;
            EffectScope scope = new EffectScope(this,evt,++TriggerCount);
            foreach(var effect in Buff.Effects)
            {
                effect.Excute(ctx,scope);
            }

        }

        public void OnRemoved(BuffContext ctx, BuffRemoveReason reason)
        {
            foreach(var trigger in Buff.Triggers)
            {
                trigger.Unbind(this, ctx);
            }
            foreach (var effect in Buff.Effects)
            {
                effect.OnExit(ctx);
            }
            IsExpired = true;
            Owner.OnBuffDetached(this);
        }
        /// <summary>
        /// 每回合触发，但仅限于维护内部参数，如剩余时间(要考虑OnTick与回合制触发效果的先后顺序
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="time"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void OnTick(BuffContext ctx, int time)
        {
            if (IsExpired) return;
            IsExpired = Buff.Duration.OnTick(this, ctx, time);
            if(IsExpired)
            {
                OnRemoved(ctx, BuffRemoveReason.Expired);
            }
        }

        public void Refresh(BuffContext ctx, int? override_time = null)
        {
            Buff.Duration.OnRefresh(this, ctx, override_time);
        }

        public void SetStacks(BuffContext ctx, int delta)
        {
            Stack = Math.Max(0, delta);
            Buff.Duration.OnStackChanged(this, ctx);
        }
    }
}

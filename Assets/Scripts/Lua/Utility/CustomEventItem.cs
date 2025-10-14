using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using XLuaTest.GameLuaTest;
namespace Lua.Utility
{
    /// <summary>
    /// 自定义事件基类
    /// </summary>
    public abstract class CustomEventItem
    {
        public abstract string EventType {get;}
        public bool enable;
        /// <summary>
        /// 函数上下文
        /// </summary>
        public void Init(LuaTable context)
        {
            enable =  context.Get<bool>("enable");
            ActionInit(context);
        }
        protected abstract void ActionInit(LuaTable context);
        public abstract CustomEventItem Clone();
        public abstract void Subscribe(LuaEventComponent luaTable);
    }




    /// <summary>
    /// 直接输出一个信息
    /// </summary>
    public class DebugEvent : CustomEventItem
    {
        public class DebugEventInfo : IEvent
        {
            public string ctx;
        }
        [CSharpCallLua]
        public delegate void DebugEventDelegate(string value);
        public override string EventType => "Debug";
        public DebugEventDelegate action;
        protected override void ActionInit(LuaTable context)
        { 
            action= context.Get<DebugEventDelegate>("action");
        }
        public override CustomEventItem Clone()
        {
            return new DebugEvent();
        }
        public override void Subscribe(LuaEventComponent luaTable)
        {
            luaTable.Subscribe<DebugEventInfo>(CallBack);
        }
        public void CallBack(DebugEventInfo info)
        {
            action(info.ctx);
        }
    }

    public class OnActorDestroyEvent : CustomEventItem
    {
        public class ActorDestroyInfo : IEvent
        {
            public string uuid;
        }
        [CSharpCallLua]
        public delegate void OnActorDestroyDelegate(string uuid);
        public override string EventType => "ActorDestroy";
        public OnActorDestroyDelegate action;
        protected override void ActionInit(LuaTable context)
        { 
            action= context.Get<OnActorDestroyDelegate>("action");
        }
        public override CustomEventItem Clone()
        {
            return new OnActorDestroyEvent();
        }
        public override void Subscribe(LuaEventComponent luaTable)
        {
            luaTable.Subscribe<ActorDestroyInfo>(CallBack);
        }
        public void CallBack(ActorDestroyInfo info)
        {
            action(info.uuid);
        }
    }
    
    public interface LuaEventFactory
    {
        public CustomEventItem CreatLuaEvent(LuaTable context);
    }
    
    /// <summary>
    /// 临时lua事件翻译器(就是一个工厂实际上)
    /// </summary>
    public class SimpleLuaEventFactory : LuaEventFactory
    {
        private Dictionary<string, CustomEventItem> eventMap;
        /// <summary>
        /// 硬编码
        /// </summary>
        public SimpleLuaEventFactory()
        {
            eventMap = new Dictionary<string, CustomEventItem>();
            AddEvent(new DebugEvent());
            AddEvent(new OnActorDestroyEvent());
        }
        private void AddEvent(CustomEventItem eventItem)
        {
            eventMap.Add(eventItem.EventType, eventItem);
        }
        public CustomEventItem CreatLuaEvent(LuaTable context)
        {
            string eventType = context.Get<string>("eventType");
            if (eventType!=null && eventMap.ContainsKey(eventType))
            {
                var tempEvent= eventMap[eventType].Clone();
                tempEvent.Init(context);
                return tempEvent;
            }
            throw new LuaEventTypeNotFoundException($"eventType:{eventType} not found");
        }
    }

    public class LuaEventTypeNotFoundException : Exception
    {
        public LuaEventTypeNotFoundException(string message) : base(message)
        {
            
        }
    }
    
}
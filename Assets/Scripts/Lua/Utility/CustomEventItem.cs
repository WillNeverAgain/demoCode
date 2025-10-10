using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace Lua.Utility
{
    /// <summary>
    /// 自定义事件基类
    /// </summary>
    public abstract class CustomEventItem
    {
        public abstract string EventType {get;}
        public string EventName;
        public bool enable;
        /// <summary>
        /// 函数上下文
        /// </summary>
        public void Init(LuaTable context)
        {
            EventName = context.Get<string>("eventName");
            enable =  context.Get<bool>("enable");
            ActionInit(context);
        }
        protected abstract void ActionInit(LuaTable context);
        public abstract CustomEventItem Clone();
    }

    /// <summary>
    /// 直接输出一个信息
    /// </summary>
    public class DebugEvent : CustomEventItem
    {
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
using System;
using System.Collections.Generic;
using Lua.Utility;
using MyFrame.FightSystem.Event;
using UnityEngine;
using XLua;
namespace XLuaTest.GameLuaTest
{
    /// <summary>
    /// 中间层转发给EventBus用的，解析lua的事件
    /// </summary>
    public class LuaEventComponent 
    {
                
        IEventBusCore eventBusCore;
        List<IDisposable> _disposables = new List<IDisposable>();
        /// <summary>
        /// 注入
        /// </summary>
        public LuaEventComponent(IEventBusCore bus, LuaTable scriptScopeTable)
        {
            eventBusCore = bus;
            SimpleLuaEventFactory factory = new SimpleLuaEventFactory();
            LuaTable EventTables = scriptScopeTable.Get<LuaTable>("EventItems");
            
            foreach (var key in EventTables.GetKeys())
            {
                LuaTable item = EventTables.Get<LuaTable>(key);
                try
                {
                    var customEvent = factory.CreatLuaEvent(item);
                    if (customEvent.enable)
                    {
                        customEvent.Subscribe(this);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
           _disposables.Add( eventBusCore.Subscribe(handler));
        }
    }
}
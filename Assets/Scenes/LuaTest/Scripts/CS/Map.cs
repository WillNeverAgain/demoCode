using System;
using System.Collections.Generic;
using MyFrame.FightSystem.Event;
using UnityEngine;
using XLua;
namespace XLuaTest.GameLuaTest
{
    // 核心层：Actor.cs
// IMapCreator.cs - 地图创建接口
    [CSharpCallLua]
    public interface IMapCreator
    {
        /// <summary>
        /// 初始化地图变量
        /// </summary>
        void InitVars();
    
        /// <summary>
        /// 初始化地图配置
        /// </summary>
        void InitMap();
        /// <summary>
        /// 放回map
        /// </summary>
        Map ReturnMap();

        void InItEvent();
    }
    public enum EventTypes
    {
        OnCreateActor,
        OnDestroyActor,
    }
    
// Map.cs - 地图核心类
    [LuaCallCSharp]
    public class Map 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Actor> Actors { get; } = new List<Actor>();

        public IEventBusCore EventBusCore;
        public Map( )
        {
            EventBusCore = new EventBusCore();
        }
        public static Map CreateMap()
        {
            return new Map(); 
        }
        public static Map GetCurrentMapInfo()
        {
            return  GameObject.Find("MapInfo").GetComponent<TestMapMono>().map;
        }
        // 创建角色接口
        public void CreateActor(string uuid, float x, float y)
        {
            Actors.Add(new Actor(uuid, x, y));
            Debug.Log($"Created actor {uuid} at ({x}, {y})");
        }

        // 设置地图属性
        public void SetProperty(string key, object value)
        {
            // 实际项目中会使用字典存储
            Debug.Log($"Set map property: {key} = {value}");
        }
    }

// Actor.cs
    [XLua.LuaCallCSharp]
    public class Actor
    {
        public string Uuid { get; }
        public Vector2 Position { get; }
        public string Type { get; set; } = "NPC";
    
        public Actor(string uuid, float x, float y)
        {
            Uuid = uuid;
            Position = new Vector2(x, y);
        }
    }
}
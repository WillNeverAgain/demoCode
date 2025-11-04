using System.Collections;
using UnityEngine;
using XLua;
using Lua.Utility;

namespace XLuaTest.GameLuaTest
{
    public class TestLua : MonoBehaviour
    {
        public TextAsset luaScript;
        internal static LuaEnv luaEnv = new LuaEnv(); // Shared Lua environment

        void Start()
        {
            // 创建新环境并设置元表继承全局
            LuaTable scriptScopeTable = luaEnv.NewTable();
            
            // 关键修复：设置元表，使环境继承全局变量
            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global); // 链接到全局环境
            scriptScopeTable.SetMetaTable(meta);
            
            // 执行Lua脚本
            luaEnv.DoString(luaScript.text, luaScript.name, scriptScopeTable);
            
            SimpleLuaEventFactory factory = new SimpleLuaEventFactory();
            LuaTable EventTables = scriptScopeTable.Get<LuaTable>("EventItems");
            
            foreach (var key in EventTables.GetKeys())
            {
                LuaTable item = EventTables.Get<LuaTable>(key);
                var customEvent = factory.CreatLuaEvent(item);
                
                // 测试调用 - 添加错误处理
                try
                {
                    if (customEvent is DebugEvent debugEvent)
                    {
                        debugEvent.action("233"); // 直接传递字符串，无需ToString()
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Event execution failed: {ex.Message}");
                }
            }
        }

        void OnDestroy()
        {
            // 清理Lua环境
            if (luaEnv != null)
            {
                luaEnv.Dispose();
            }
        }
    }
}
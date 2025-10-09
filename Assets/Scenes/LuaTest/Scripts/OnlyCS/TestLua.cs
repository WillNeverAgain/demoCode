using System.Collections;
using System.Collections.Generic;
using Lua.Utility;
using UnityEngine;
using XLua;

namespace XLuaTest.GameLuaTest
{
    public class TestLua : MonoBehaviour
    {
        
        public TextAsset luaScript;
        internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
        // Start is called before the first frame update
        void Start()
        {
            // 为每个脚本设置一个独立的脚本域，可一定程度上防止脚本间全局变量、函数冲突
            LuaTable  scriptScopeTable = luaEnv.NewTable();
            luaEnv.DoString(luaScript.text, luaScript.name, scriptScopeTable);
            SimpleLuaEventTranslator translator = new SimpleLuaEventTranslator();
            LuaTable EventTables =  scriptScopeTable.Get<LuaTable>("EventItems");
            foreach (var temp in EventTables.GetKeys())
            {
                LuaTable item = EventTables.Get<LuaTable>(temp);
                var customFunc= translator.CreatLuaEvent(item);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}


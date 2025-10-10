using System;
using Lua.Utility;
using UnityEngine;
using XLua;
namespace XLuaTest.GameLuaTest
{
    public class TestMapMono : MonoBehaviour
    {
        public TextAsset luaScript;
        internal static LuaEnv luaEnv = new LuaEnv(); // Shared Lua environment

        void Start()
        {
            // 添加自定义加载器：从Resources加载Lua文件
            luaEnv.AddLoader((ref string filepath) => {
                // 转换模块路径：例如"baseMap" -> "Lua/baseMap"
                string resourcePath = "Lua/" + filepath.Replace('.', '/');
                TextAsset luaFile = Resources.Load<TextAsset>(resourcePath);
                if (luaFile != null)
                {
                    return System.Text.Encoding.UTF8.GetBytes(luaFile.text);
                }
                return null; // 找不到时返回null，Lua会报错
            });

            // 创建新环境并设置元表继承全局
            LuaTable scriptScopeTable = luaEnv.NewTable();
            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global); // 链接到全局环境
            scriptScopeTable.SetMetaTable(meta);

            // 执行主Lua脚本
            object[] result = luaEnv.DoString(luaScript.text, luaScript.name, scriptScopeTable);
            if (result != null && result.Length > 0)
            {
                LuaTable table = (LuaTable)result[0];
                foreach (var obj in table.GetKeys())
                {
                    Debug.Log($"key {obj}: {table.Get<object>(obj).GetType().FullName}");
                    if(obj.Equals("ReturnMap"))
                    {
                        var func = table.Get<LuaFunction>("ReturnMap");
                        var temp= func.Call();
                        if (temp != null && temp[0] != null)
                        {
                            Debug.Log(temp[0].GetType().ToString());
                        }
                    }
                }
                IMapCreator te =  table.Cast<IMapCreator>(); ;
                te.InitVars();
                te.InitMap();
                Map map = te.ReturnMap();
                foreach (var actor in map.Actors)
                {
                    Debug.Log($"Actor UUID: {actor.Uuid}, Position: {actor.Position}");
                }
            }
            else
            {
                Debug.LogError("Lua script did not return a valid IMapCreator object.");
            }
        }

    }
}
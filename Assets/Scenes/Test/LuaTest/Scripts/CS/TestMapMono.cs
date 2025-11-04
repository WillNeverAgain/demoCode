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

        [SerializeField] private GameObject floor,actorPrefab;
        public Map map;

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
                IMapCreator te =  table.Cast<IMapCreator>(); ;
                te.InitVars();
                te.InitMap();
                te.InItEvent();
                map = te.ReturnMap();
                LuaEventComponent temp = new LuaEventComponent(map.EventBusCore,table);
                CreatMap();

                foreach (var actor in map.Actors)
                {
                    Debug.Log($"Actor UUID: {actor.Uuid}, Position: {actor.Position}");
                   var  tempActor=Instantiate(actorPrefab,new Vector3(actor.Position.x,1.4f,actor.Position.y),Quaternion.identity);
                   tempActor.GetComponent<TestCapule>().actor = actor;
                }
            }
            else
            {
                Debug.LogError("Lua script did not return a valid IMapCreator object.");
            }
        }
        private void CreatMap()
        {
            for (int i = -10; i < 10; i++)
            {
                for (int z = -10; z < 10; z++)
                {
                    Instantiate(floor, new Vector3(i, 0, z),Quaternion.identity);
                }
            }
        }
        public void DebugButton()
        {
            map.EventBusCore.Publish(new DebugEvent.DebugEventInfo(){ctx="AAAAAAbug批发"});
        }
    }
}
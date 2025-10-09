using System;
using XLua;
namespace XLuaTest.GameLuaTest
{

    public interface Map
    {
        /// <summary>
        /// 地图长宽
        /// </summary>
        public int wide { get; }
        public int high { get; }
        public void InItVar();
        public void InjectVar(string varPath,object value);
        public void InitEvents();
    }
}
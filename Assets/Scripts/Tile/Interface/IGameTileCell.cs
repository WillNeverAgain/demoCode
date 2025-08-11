using System.Collections.Generic;
using UnityEngine;
namespace Tile
{
    /// <summary>
    /// 游戏Tile的Cell类
    /// 不包括Tile上的物体或者其它什么
    /// 从管理类调用，或者生命周期类调用
    /// 其它对象不调用
    /// </summary>
    public interface IGameTileCell : ITagObject
    {
        /// <summary>
        /// 只在View里调用
        /// 其它物体不调用
        /// </summary>
        internal void Render(IGameMapRefreshContext tileObject);
        public IList<IGameTileObject> GameTileObject { get; set; }
    }
}
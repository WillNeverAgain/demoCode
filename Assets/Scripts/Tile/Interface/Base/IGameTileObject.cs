using UnityEngine;
namespace Tile.Base
{
    /// <summary>
    /// 非地块的地图物品
    /// </summary>
    public interface IGameTileObject : ITagObject
    {
        /// <summary>
        /// 网格位置
        /// </summary>
        public Vector2Int CellPosition { get; set; }
        /// <summary>
        /// 只在View里调用
        /// 其它物体不调用
        /// </summary>
        internal void Render(Vector2Int position, IGameMapRefreshContext refreshContext);
    }
}
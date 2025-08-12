using UnityEngine;
namespace Tile
{
    /// <summary>
    /// 非地块的地图物品
    /// </summary>
    public interface IGameTileObject : ITagObject
    {
        public delegate void TilePositionChangedDelegate(Vector2Int oldPosition, Vector2Int newPosition);
        
        /// <summary>
        /// 需要在实现里手动调用
        /// 外部只注册
        /// </summary>
        public event TilePositionChangedDelegate OnTileLogicalPositionChanged;

        /// <summary>
        /// 视图位置 动画结束之后刷新
        /// </summary>
        public event TilePositionChangedDelegate OnTileViewPositionChanged;

        
        /// <summary>
        /// 逻辑位置 
        /// </summary>
        public Vector2Int LogicalPosition { get; set; }
        /// <summary>
        /// 视图位置
        /// </summary>
        public Vector2Int ViewPosition { get; set; }
        /// <summary>
        /// 只在View里调用
        /// 其它物体不调用
        /// </summary>
        internal void Render(Vector2Int position, IGameMapRefreshContext refreshContext);
    }
}
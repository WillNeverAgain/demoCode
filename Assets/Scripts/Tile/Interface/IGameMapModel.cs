using System.Collections.Generic;
using UnityEngine;
namespace Tile
{
    /// <summary>
    /// 游戏地图资源的数据类
    /// 提供游戏的地图数据
    /// 外部使用不直接只有，仅在系统内调用
    /// 外部调用通过 Coordinator
    /// </summary>
    public interface IGameMapModel
    {
        /// <summary>
        /// TODO: 暂留通用初始化接口，后期扩展
        /// </summary>
        public void Initialize(params object[] info);
        /// <summary>
        /// 默认信息
        /// </summary>
        IGameTileCell DefaultCell { get; }
        /// <summary>
        /// 地面信息
        /// </summary>
        IGameTileCell[,] GameTileCells { get; }
        
        /// <summary>
        /// 获得地板
        /// </summary>
        public IGameTileCell GetCell(int x, int y);
        public IGameTileCell GetCell(Vector2Int logicalPosition);

        /// <summary>
        /// 获得物体
        /// </summary>
        public IList<IGameTileObject> GetObjects(int x, int y);
        public IList<IGameTileObject> GetObjects(Vector2Int logicalPosition);
        
        /// <summary>
        /// 获得有tag的物体
        /// </summary>
        public IList<ITagObject> GetObjectTags(int x, int y,string tagName);
        public IList<ITagObject> GetObjectTags(Vector2Int logicalPosition,string tagName);

    }
}
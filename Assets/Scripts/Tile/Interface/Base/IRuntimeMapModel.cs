using System.Collections.Generic;
using UnityEngine;
namespace Tile.Base
{
    /// <summary>
    /// 游戏地图资源的数据类
    /// 提供游戏的地图数据
    /// 外部使用不直接只有，仅在系统内调用
    /// 外部调用通过 Coordinator
    /// 暂时只提供检索功能
    ///
    /// 只存储数据，需要用content转换成游戏的上下文
    /// </summary>
    public interface IRuntimeMapModel
    {
        /// <summary>
        /// 地图的名字
        /// </summary>
        public string MapName { get;  }
        /// <summary>
        /// TODO: 暂留通用初始化接口，后期扩展
        /// </summary>
        public IRuntimeMapModel Initialize(params object[] info);
        /// <summary>
        /// 默认信息
        /// </summary>
        IGameTileCell DefaultCell { get; }
        public int MapWidth { get; }

        public int MapHight { get; }
        /// <summary>
        /// 获得地板
        /// </summary>
        public IGameTileCell GetCell(int x, int y);
        public IGameTileCell GetCell(Vector2Int cellPosition);
        /// <summary>
        /// 获得物体
        /// </summary>
        public IReadOnlyList<ITagObject> GetCellAndObjects(int x, int y);
        public IReadOnlyList<ITagObject> GetCellAndObjects(Vector2Int cellPosition);
    }
}
using Tile.Interface.Base;
using UnityEngine;
namespace Tile.Base
{
    /// <summary>
    /// 空接口用来标记
    /// 预留后期扩展
    /// 刷新地图信息的时候的上下文
    /// 只作为流通的信息类
    /// 在管理器内初始化
    /// 并传入其它类
    /// </summary>
    public interface IGameMapRefreshContext : IMapContext
    {
            void Initialize(ICellFactory factory,IGameMapPositionContext positionContext);
            /// <summary>
            /// 刷新时给cell类提供实例化工厂
            /// </summary>
            ICellFactory CellFactory { get; }
            /// <summary>
            /// 提供位置转换的工具
            /// </summary>
            IGameMapPositionContext PositionContext { get; }
    }

    
}
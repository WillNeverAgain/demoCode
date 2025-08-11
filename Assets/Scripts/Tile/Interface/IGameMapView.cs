using UnityEngine;
namespace Tile
{   
    /// <summary>
    /// 视觉层相关的东西
    /// </summary>
    public interface IGameMapView
    {
        public void Initialize(IGameMapModel gameMapModel);
        /// <summary>
        /// 调用后会绘制地图
        /// 外部调用接口 不传入数据
        /// 由生命周期相关的系统调用
        /// </summary>
        public void Render();
        /// <summary>
        /// 世界坐标转换成cell坐标
        /// </summary>
        public void WorldPosToCell(Vector3 position);
        
        /// <summary>
        /// 网格坐标转换成世界坐标
        /// </summary>
        public void CellPosToWorld(Vector2Int pos);
    }
}
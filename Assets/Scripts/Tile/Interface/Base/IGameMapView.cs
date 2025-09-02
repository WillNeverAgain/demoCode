using UnityEngine;
namespace Tile.Base
{   
    /// <summary>
    /// 视觉层相关的东西
    /// </summary>
    public interface IGameMapView
    {
        /// <summary>
        /// 初始化绑定方法
        /// </summary>
        public IGameMapView Initialize(IGameMapModel gameMapModel,IGameMapRefreshContext ctx ,params object[] param);
        /// <summary>
        /// 调用后会绘制地图
        /// 外部调用接口 不传入数据
        /// 由生命周期相关的系统调用
        /// </summary>
        public void Render();
    }
}
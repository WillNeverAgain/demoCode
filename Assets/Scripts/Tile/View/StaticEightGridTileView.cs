using Tile.Base;
using UnityEngine;
namespace Tile.View
{
    /// <summary>
    ///     手动搭建的静态网格View类
    ///     Demo1使用的初版
    ///     主要是用来测试
    /// </summary>
    public class StaticEightGridTileView : IGameMapView
    {
        private IRuntimeMapModel _runtimeMapModel;
        private IGameMapRefreshContext _gameMapRefreshContext;
        public IGameMapView Initialize(
                                IRuntimeMapModel runtimeMapModel,
                               IGameMapRefreshContext ctx, 
                               params object[] param)
        {
            _runtimeMapModel = runtimeMapModel;
            _gameMapRefreshContext = ctx;
            return this;
        }
        public void Render()
        {
            for (int x = 0; x < _runtimeMapModel.MapWidth; x++)
            {
                for (int y = 0; y < _runtimeMapModel.MapHight; y++)
                {
                    _runtimeMapModel.GetCell(x,y).Render(x,y, _gameMapRefreshContext);
                }
            }
        }

    }
}
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
        private IGameMapModel _gameMapModel;
        private IGameMapRefreshContext _gameMapRefreshContext;
        public IGameMapView Initialize(
                                IGameMapModel gameMapModel,
                               IGameMapRefreshContext ctx, 
                               params object[] param)
        {
            _gameMapModel = gameMapModel;
            _gameMapRefreshContext = ctx;
            return this;
        }
        public void Render()
        {
            for (int x = 0; x < _gameMapModel.MapWidth; x++)
            {
                for (int y = 0; y < _gameMapModel.MapHight; y++)
                {
                    _gameMapModel.GetCell(x,y).Render(x,y, _gameMapRefreshContext);
                }
            }
        }

    }
}
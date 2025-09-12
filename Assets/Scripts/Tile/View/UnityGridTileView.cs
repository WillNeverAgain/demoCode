using Tile.Base;
using UnityEngine;
namespace Tile.View
{
    public class UnityGridTileView : IGameMapView
    {
        private IRuntimeMapModel _model;
        private IGameMapRefreshContext _refreshContext;
        public IGameMapView Initialize(IRuntimeMapModel runtimeMapModel, IGameMapRefreshContext ctx, params object[] param)
        {
            _model = runtimeMapModel;
            _refreshContext = ctx;
            return this;
        }
        public void Render()
        {
            for (int x = 0; x < _model.MapWidth; x++)
            {
                for (int y = 0; y < _model.MapHight; y++)
                {
                    _model.GetCell(x,y).Render(x,y, _refreshContext);
                }
            }
        }
    }
}
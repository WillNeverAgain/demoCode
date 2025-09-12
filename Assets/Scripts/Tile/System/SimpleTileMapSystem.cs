using System.Collections.Generic;
using Tile.Base;
using Tile.Context;
using Tile.Factory;
using UnityEngine;
namespace Tile
{
    public class SimpleTileMapSystem : ITileMapSystem
    {
        private IRuntimeMapModel _runtimeMapModel;
        private IGameMapView _mapView;
        private IMiddlewareFactory _middlewareFactory;
        /// <summary>
        /// 第一版直接注入吧
        /// </summary>
        /// <param name="model"></param>
        /// <param name="view"></param>
        /// <param name="factory"></param>
        public SimpleTileMapSystem(IRuntimeMapModel model,IGameMapView view,IMiddlewareFactory factory)
        {
            _runtimeMapModel=model;
            _mapView=view;
            _middlewareFactory=factory;
            //链接上数据
            // factory.Connect(model);
        }
        public T GetMiddleware<T>() where T : IMapMiddleware
        {
            return _middlewareFactory.GetMiddleware<T>();
        }
        public ITileMapSystem Initialize(params object[] args)
        {
            IGameMapPositionContext positionContext = new RhombusGridTilePositionContext();
            ICellFactory factory = new SimpleTIleCellFactory();
            IGameMapRefreshContext refreshContext = new SimpleRefreshContext();
            refreshContext.Initialize(factory,positionContext);
            _mapView.Initialize(_runtimeMapModel,refreshContext);
            return this;
        }
        public void Update(float deltaTime)
        {
            
        }
    }
}
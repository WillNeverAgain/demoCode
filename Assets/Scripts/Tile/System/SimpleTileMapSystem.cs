using System.Collections.Generic;
using Tile.Base;
using Tile.Context;
using Tile.Factory;
using UnityEngine;
namespace Tile
{
    public class SimpleTileMapSystem : ITileMapSystem
    {
        private IGameMapModel _gameMapModel;
        private IGameMapView _mapView;
        private IMiddlewareFactory _middlewareFactory;
        /// <summary>
        /// 第一版直接注入吧
        /// </summary>
        /// <param name="model"></param>
        /// <param name="view"></param>
        /// <param name="factory"></param>
        public SimpleTileMapSystem(IGameMapModel model,IGameMapView view,IMiddlewareFactory factory)
        {
            _gameMapModel=model;
            _mapView=view;
            _middlewareFactory=factory;
            //链接上数据
            factory.Connect(model);
        }
        public ITileMapSystem Initialize(params string[] args)
        {
            IGameMapPositionContext positionContext = new RhombusGridTilePositionContext();
            ICellFactory factory = new SimpleTIleCellFactory();
            IGameMapRefreshContext refreshContext = new SimpleRefreshContext();
            refreshContext.Initialize(factory,positionContext);
            _mapView.Initialize(_gameMapModel,refreshContext);
            return this;
        }
        public IGameTileCell GetCell(int x, int y)
        {
            return _gameMapModel.GetCell(x, y);
        }
        public IGameTileCell GetCell(Vector2Int logicalPosition)
        {
            return _gameMapModel.GetCell(logicalPosition);
        }
        public IList<IGameTileObject> GetObjects(int x, int y)
        {
            return _gameMapModel.GetObjects(x, y);            
        }
        public IList<IGameTileObject> GetObjects(Vector2Int logicalPosition)
        {
            return _gameMapModel.GetObjects(logicalPosition);
        }
        public T GetMiddleware<T>() where T : IMiddleware
        {
            return _middlewareFactory.GetMiddleware<T>();
        }
        public void Update(float deltaTime)
        {
            
        }
    }
}
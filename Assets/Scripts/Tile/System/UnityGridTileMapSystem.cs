using System.Collections.Generic;
using Tile.Base;
using Tile.Context;
using Tile.Factory;
using UnityEngine;
namespace Tile
{
    public class UnityGridTileMapSystem : ITileMapSystem
    {

        private IGameMapModel _gameMapModel;
        private IGameMapView _mapView;
        private IMiddlewareFactory _middlewareFactory;
        
        public UnityGridTileMapSystem(IGameMapModel model,IGameMapView view,
                                      IMiddlewareFactory factory)
        {
            _gameMapModel=model;
            _mapView=view;
            _middlewareFactory=factory;
        }
        /// <summary>
        /// Demo版本硬编码
        /// </summary>
        public ITileMapSystem Initialize(params string[] args)
        {

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
            _mapView.Render();
        }
    }
}
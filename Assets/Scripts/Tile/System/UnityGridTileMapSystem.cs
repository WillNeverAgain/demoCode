using System;
using System.Collections.Generic;
using Tile.Base;
using Tile.Context;
using Tile.Factory;
using UnityEngine;
namespace Tile
{
    public class UnityGridTileMapSystem : ITileMapSystem , IMiddlewareGetter
    {

        private IGameMapModel _gameMapModel;
        private IGameMapView _mapView;
        private IMiddlewareFactory _middlewareFactory;
        
        private Dictionary<Type,IMapContext> _mapContexts = new Dictionary<Type,IMapContext>();
        
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
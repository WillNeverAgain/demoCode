using System;
using Tile;
using Tile.Base;
using Tile.Context;
using Tile.Factory;
using Tile.Middleware.Factory;
using Tile.Model;
using Tile.View;
using UnityEngine;
namespace UniTest.Demo1
{
    public class Demo1MonoTileSystem : MonoBehaviour
    {
        private  ITileMapSystem _tileMapSystem;
        [Header("Tile 系统设置")]
        [SerializeField]
        private Grid _grid;
        [SerializeField]
        private GameObject _tilePrefab;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            IGameMapPositionContext positionContext = new UnityGridTilePositionContext(_grid);
            positionContext.Initialize(Vector3.zero);
            ICellFactory factory = new UnityGridCellFactory();
            factory.Initialize(_grid.transform,_tilePrefab);
            IGameMapRefreshContext refreshContext = new SimpleRefreshContext();
            refreshContext.Initialize(factory,positionContext);

            IGameMapModel teModel = new StaticFourGridTileModel();
            teModel.Initialize();
            
            IGameMapView mapView = new UnityGridTileView();
            mapView.Initialize(teModel, refreshContext);

            IMiddlewareFactory middlewareFactory = new SimpleMiddlewareFactory();
            middlewareFactory.Connect(teModel);
                
            _tileMapSystem = new UnityGridTileMapSystem(
                teModel,
                mapView,
                middlewareFactory);
        }
        private void Update()
        {
            _tileMapSystem.Update(Time.deltaTime);
            //TODO: 中间件测试
        }
    }
}
using Tile.Base;
using Tile.Context;
using Tile.Factory;
using Tile.Middleware.Factory;
using Tile.Model;
using Tile.View;
using Tool;
using UnityEngine;
namespace Tile
{
    public class UnityGridTileMapSystem : ITileMapSystem  
    {

        private ArgumentParser argumentParser;
        
        private IGameMapModel _gameMapModel;
        private IGameMapView _mapView;
        private IMiddlewareFactory _middlewareFactory;
        private IMapContextBlackboard _mapContextBlackboard;
        
        public UnityGridTileMapSystem()
        { 
            argumentParser = new ArgumentParser();
          argumentParser.SettingArguments<Grid>("unity的网格组件");
          argumentParser.SettingArguments<GameObject>("网格物体预制件");
        }
        public T GetMiddleware<T>() where T : IMapMiddleware
        {
            return _middlewareFactory.GetMiddleware<T>();
        }
        /// <summary>
        /// Demo版本硬编码
        /// </summary>
        public ITileMapSystem Initialize(params object[] args)
        {
            Grid _grid = argumentParser.ParserArguments<Grid>(args[0]);
            GameObject _tilePrefab = argumentParser.ParserArguments<GameObject>(args[1]);
            
            IMapContextBlackboard blackboard = new DefaultMapContextBlackboard();
            
            IGameMapPositionContext positionContext = new UnityGridTilePositionContext(_grid);
            positionContext.Initialize(new Vector3(0.5f, 0));
            
            blackboard.RegisterContext<IGameMapPositionContext>(positionContext);
            
            ICellFactory factory = new UnityGridCellFactory();
            factory.Initialize(_grid.transform,_tilePrefab);

            IGameMapRefreshContext refreshContext = new SimpleRefreshContext();
            refreshContext.Initialize(factory,positionContext);

            blackboard.RegisterContext<IGameMapRefreshContext>(refreshContext);

            IGameMapModel teModel = new StaticFourGridTileModel();
            teModel.Initialize();

            IGameMapObjectContext mapObjectContext = new  DefaultGameMapObjectContext(teModel);
            blackboard.RegisterContext<IGameMapObjectContext>(mapObjectContext);
            
            IGameMapView mapView = new UnityGridTileView();
            mapView.Initialize(teModel, refreshContext);

            IMiddlewareFactory middlewareFactory = new SimpleMiddlewareFactory();
            middlewareFactory.Initialize(blackboard);
            
            
            
            
            _gameMapModel = teModel;
            _middlewareFactory = middlewareFactory;
            _mapView = mapView;
            _mapContextBlackboard = blackboard;
            
            return this;
        }
        public void Update(float deltaTime)
        {
            _mapView.Render();
        }
    }
}
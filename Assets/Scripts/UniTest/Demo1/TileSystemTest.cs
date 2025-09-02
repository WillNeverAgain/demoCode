using Tile;
using Tile.Middleware.Factory;
using Tile.Model;
using Tile.View;
using UnityEngine;
namespace UniTest.Demo1
{
    [UniTest]
    public  class TileSystemTest
    {
        private static ITileMapSystem _tileMapSystem;
        public static void Initialize()
        {
            if (_tileMapSystem == null)
            {
                _tileMapSystem = new SimpleTileMapSystem(
                    new StaticFourGridTileModel(),
                    new StaticEightGridTileView(),
                    new SimpleMiddlewareFactory());
                _tileMapSystem.Initialize();
            }
        }
        public static void UniTest1()
        {
            _tileMapSystem.GetCell(1, 1);
            
        }
    }
}
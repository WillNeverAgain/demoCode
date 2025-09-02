using System.Collections.Generic;
using System.Linq;
using Tile.Base;
using Tile.TillCell.Demo1;
using UnityEngine;
namespace Tile.Model
{
    /// <summary>
    ///     手动搭建的静态网格数据
    ///     Demo1使用的初版
    ///     主要是用来测试
    /// </summary>
    public class StaticFourGridTileModel : IGameMapModel
    {
        private IGameTileCell[,] _gameTileCells;
        private int _cell_wide=>_gameTileCells.GetLength(0);
        private int _cell_high=>_gameTileCells.GetLength(1);

        public IGameMapModel Initialize(params object[] info)
        {
            _gameTileCells = new IGameTileCell[,]
            {
                { new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell() },
                { new GroundCell(), new DarkCell() , new DarkCell() , new GroundCell() , new GroundCell() },
                { new GroundCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell() },
                { new GroundCell(), new DarkCell() , new DarkCell() , new DarkCell() , new GroundCell() },
                { new GroundCell(), new DarkCell() , new GroundCell() , new DarkCell() , new GroundCell() },
                { new GroundCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell() },
            };
            return this;
        }
        public IGameTileCell DefaultCell {
            get {
                return new DarkCell();
            }
        }
        public IGameTileCell[,] GameTileCells => _gameTileCells;
        public int MapWidth => _cell_wide;
        public int MapHight => _cell_high;
        public IGameTileCell GetCell(int x, int y)
        {
            if (x+y*_cell_wide >= _gameTileCells.Length)
            {
                return   DefaultCell;
            }
            return _gameTileCells[x, y];
        }
        public IGameTileCell GetCell(Vector2Int cellPosition)
        {
            return GetCell(cellPosition.x, cellPosition.y);
        }
        public IList<IGameTileObject> GetObjects(int x, int y)
        {
            return GetCell(x, y).GameTileObject;
        }
        public IList<IGameTileObject> GetObjects(Vector2Int cellPosition)
        {
            return GetCell(cellPosition).GameTileObject;
        }
        public IList<ITagObject> GetObjectTags(int x, int y, string tagName)
        {
            return GetCell(x,y).GameTileObject.
                Where(obj=>obj.Tags.Contains(tagName))
                .Select(a=>a as ITagObject)
                .ToList();
        }
        public IList<ITagObject> GetObjectTags(Vector2Int cellPosition, string tagName)
        {
            return GetCell(cellPosition).GameTileObject.
                Where(obj=>obj.Tags.Contains(tagName))
                .Select(a=>a as ITagObject)
                .ToList();
        }
    }
}
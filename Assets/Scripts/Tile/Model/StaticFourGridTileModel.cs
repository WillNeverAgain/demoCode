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
    public class StaticFourGridTileModel : IRuntimeMapModel
    {
        private IGameRuntimeTileCell[,] _gameTileCells;
        private int _cell_wide=>_gameTileCells.GetLength(0);
        private int _cell_high=>_gameTileCells.GetLength(1);

        public string MapName => "StaticFourGridTileModel";
        public IRuntimeMapModel Initialize(params object[] info)
        {
            _gameTileCells = new IGameRuntimeTileCell[,]
            {
                {
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                {
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                               new GroundCell(), new GroundCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                               new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                               new DarkCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                               new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new GroundCell(), new GroundCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new DarkCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new DarkCell() , new GroundCell()
                },
                           {
                         new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new DarkCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new DarkCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new DarkCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new DarkCell() , new GroundCell() , new DarkCell() , new DarkCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new DarkCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new GroundCell(), new GroundCell() , new DarkCell() , new GroundCell() , new DarkCell(),
                    new GroundCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new DarkCell() , new DarkCell()
                },
                                 {
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                {
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                               new GroundCell(), new GroundCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                               new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                               new DarkCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                               new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new GroundCell(), new GroundCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new DarkCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new DarkCell() , new GroundCell()
                },
                           {
                         new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new DarkCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new DarkCell() , new DarkCell() , new GroundCell() , new GroundCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new DarkCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new DarkCell() , new GroundCell() , new DarkCell() , new DarkCell(),
                    new DarkCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new DarkCell() , new GroundCell() , new GroundCell()
                },
                           {
                         new GroundCell(), new GroundCell() , new DarkCell() , new GroundCell() , new DarkCell(),
                    new GroundCell(), new DarkCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new GroundCell() , new GroundCell(),
                    new GroundCell(), new GroundCell() , new GroundCell() , new DarkCell() , new DarkCell()
                },
            };
            return this;
        }
        public IGameRuntimeTileCell DefaultCell {
            get {
                return new DarkCell();
            }
        }
        public int MapWidth => _cell_wide;
        public int MapHight => _cell_high;
        public IGameRuntimeTileCell GetCell(int x, int y)
        {
            if (x+y*_cell_wide > _gameTileCells.Length || 
                x<0 || y<0
                || x>= _cell_wide || y>= _cell_high)
            {
                return   DefaultCell;
            }
            return _gameTileCells[x, y];
        }
        public IGameRuntimeTileCell GetCell(Vector2Int cellPosition)
        {
            return GetCell(cellPosition.x, cellPosition.y);
        }
        public IReadOnlyList<ITagObject> GetCellAndObjects(int x, int y)
        {
            return GetCell(x, y).Container.GetSelfAndAllObjects();

        }
        public IReadOnlyList<ITagObject> GetCellAndObjects(Vector2Int cellPosition)
        {
            return GetCell(cellPosition).Container.GetSelfAndAllObjects();
        }
        public IReadOnlyList<ITagObject> GetObjectTags(int x, int y, string tagName)
        {
            return GetCell(x,y).Container.GameTileObject.
                Where(obj=>obj.Tags.Contains(tagName))
                .Select(a=>a as ITagObject)
                .ToList();
        }
        public IReadOnlyList<ITagObject> GetObjectTags(Vector2Int cellPosition, string tagName)
        {
            return GetCell(cellPosition).Container.GameTileObject.
                Where(obj=>obj.Tags.Contains(tagName))
                .Select(a=>a as ITagObject)
                .ToList();
        }
    }
}
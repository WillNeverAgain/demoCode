using System;
using System.Collections.Generic;
using Tile.Base;
using Tile.ObjectContainer;
using UnityEngine;
namespace Tile.TillCell.Demo1
{
    /// <summary>
    /// 无法移动到的黑暗领域(?
    /// </summary>
    public class DarkCell : IGameTileCell
    {

        public IList<string> Tags => _tags;
        private List<string> _tags=new List<string>();

        void IGameTileCell.Render(int x, int y, IGameMapRefreshContext tileObject)
        {
            cellPosition=new Vector2Int(x,y);
        }
        public Vector2Int CellPosition => cellPosition;
        public IMapObjectContainer Container => _container;
        private IMapObjectContainer _container = new NullObjectContainer();
        private Vector2Int cellPosition = Vector2Int.zero;
        public IReadOnlyList<ITagObject> GetSelfAndAllObjects()
        {
            return null;
        }

    }
}
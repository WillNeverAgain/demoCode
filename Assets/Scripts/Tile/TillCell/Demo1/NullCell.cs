using System.Collections.Generic;
using Tile.Base;
using UnityEngine;
namespace Tile.TillCell.Demo1
{
    public class NullCell  : IGameTileCell
    {

        public IList<string> Tags {
            get;
        }
        void IGameTileCell.Render(int x, int y, IGameMapRefreshContext tileObject)
        {
            throw new System.NotImplementedException();
        }
        public Vector2Int CellPosition {
            get;
        }
        public IMapObjectContainer Container {
            get;
        }
    }
}
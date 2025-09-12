using System.Collections.Generic;
using Tile.Base;
using UnityEngine;
namespace Tile.Context
{
    public class DefaultGameMapObjectContext : IGameMapObjectContext
    {
        private IRuntimeMapModel _model;
        public DefaultGameMapObjectContext(IRuntimeMapModel model)
        {
            _model = model;
        }
        public IGameTileCell GetCell(int x, int y)
        {
            return _model.GetCell(x, y);
        }
        public IGameTileCell GetCell(Vector2Int logicalPosition)
        {
            return _model.GetCell(logicalPosition);
        }
        public IReadOnlyList<ITagObject> GetCellAndObjects(int x, int y)
        {
            return _model.GetCellAndObjects(x, y);
        }
        public IReadOnlyList<ITagObject> GetCellAndObjects(Vector2Int logicalPosition)
        {
            return _model.GetCellAndObjects(logicalPosition);
        }
    }
}
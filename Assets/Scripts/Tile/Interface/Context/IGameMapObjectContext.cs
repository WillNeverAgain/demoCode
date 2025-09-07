using System.Collections.Generic;
using UnityEngine;
namespace Tile.Base
{
    public interface IGameMapObjectContext : IMapContext
    {
        
        /// <summary>
        /// 获得地板
        /// </summary>
        public IGameTileCell GetCell(int x, int y);
        public IGameTileCell GetCell(Vector2Int logicalPosition);
        /// <summary>
        /// 得到单元格和物体
        /// </summary>
        public IReadOnlyList<ITagObject> GetCellAndObjects(int x, int y);
        public IReadOnlyList<ITagObject> GetCellAndObjects(Vector2Int logicalPosition);

    }
}
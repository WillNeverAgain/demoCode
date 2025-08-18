using System.Collections.Generic;
using UnityEngine;
namespace Tile
{
    public interface ITileMapSystem
    {
        /// <summary>
        /// 获得地板
        /// </summary>
        public IGameTileCell GetCell(int x, int y);
        public IGameTileCell GetCell(Vector2Int logicalPosition);

        /// <summary>
        /// 获得物体
        /// </summary>
        public IList<IGameTileObject> GetObjects(int x, int y);
        public IList<IGameTileObject> GetObjects(Vector2Int logicalPosition);
        
    }
}
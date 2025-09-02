using System.Collections.Generic;
using Tile.Base;
using UnityEngine;
namespace Tile.Middleware
{
    public interface IGameTileMovementMiddleware : IMiddleware
    {
        /// <summary>
        /// 检测能否移动到对应的位置
        /// </summary>
        /// <param name="tileObject">物体</param>
        /// <param name="toPosition">移动到的位置</param>
        /// <returns>移动的过程路径点</returns>
        public IEnumerator<Vector2Int> CouldMoveTo(IGameTileObject tileObject, Vector2Int toPosition);
        /// <summary>
        /// 直接进行移动
        /// </summary>
        /// <param name="tileObject">移动的物体</param>
        /// <param name="toPosition">移动到的位置</param>
        public void MoveTo(IGameTileObject tileObject, Vector2Int toPosition);
    }
}
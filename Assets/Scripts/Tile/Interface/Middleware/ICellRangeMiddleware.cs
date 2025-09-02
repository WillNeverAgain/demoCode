using System;
using System.Collections.Generic;
using Tile.Base;
using UnityEngine;
namespace Tile.Middleware
{

    /// <summary>
    /// 记录偏移量
    /// </summary>
    [Serializable]
    public struct CellRangeInfo
    {
        public Vector2Int offset;
    }
    /// <summary>
    /// 通过这个中间件实现访问信息的获取
    /// </summary>
    public interface ICellRangeMiddleware : IMiddleware
    {
    }
}
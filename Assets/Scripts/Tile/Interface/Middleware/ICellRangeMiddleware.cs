using System;
using System.Collections.Generic;
using UnityEngine;
namespace Tile
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
        public void GetObjInRange(Vector2Int startPosition, params CellRangeInfo[] cellRangeInfo);
        public void GetObjInRange(Vector2Int startPosition,IList<string> tags, params CellRangeInfo[] cellRangeInfos);
    }
}
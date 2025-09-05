using System;
using System.Collections.Generic;
using Range.Interface;
using Tile.Base;
using UnityEngine;
namespace Tile.Middleware
{

    /// <summary>
    /// 通过这个中间件实现访问信息的获取
    /// </summary>
    public interface ICellRangeMiddleware : IMiddleware
    {
        /// <summary>
        ///  获得范围内满足条件的cell
        /// </summary>
        public IReadOnlyList<IGameTileCell> GetCellWithTagInRange(Vector3 findPosition,Func<IList<string>,bool> tagCharger, IRangeArrayInfo rangeArrayInfo);
        /// <summary>
        ///  获得范围内满足条件的cell
        /// </summary>
        public IReadOnlyList<IGameTileCell> GetCellWithTagInRange(Vector3 findPosition,Func<IList<string>,bool> tagCharger, IEnumerable<Vector2Int> rangeArray);
        /// <summary>
        /// 返回范围内对应的tag
        /// </summary>
        public IReadOnlyList<ITagObject> FindObjectWithTagInRange(Vector3 findPosition,string tag, IRangeArrayInfo rangeArrayInfo);
        /// <summary>
        /// 返回判断func为真时的tag
        /// </summary>
        public IReadOnlyList<ITagObject> FindObjectWithTagInRange(Vector3 findPosition,Func<IList<string>,bool> tagCharger, IRangeArrayInfo rangeArrayInfo);
        /// <summary>
        ///  返回判断func为真时的tag
        ///  没有顺序和层级，会遍历所有range
        /// </summary>
        public IReadOnlyList<ITagObject> FindObjectWithTagInRange(Vector3 findPosition,Func<IList<string>,bool> tagCharger, IEnumerable<Vector2Int> rangeArray);
    }
}
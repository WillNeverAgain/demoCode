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
        /// 返回范围内对应的tag
        /// </summary>
        public ITagObject FindObjectWithTagInRange(string tag, IRangeArrayInfo rangeArrayInfo);
        /// <summary>
        /// 返回判断func为真时的tag
        /// </summary>
        public ITagObject FindObjectWithTagInRange(Func<bool,string> tagCharger, IRangeArrayInfo rangeArrayInfo);
        /// <summary>
        ///  返回判断func为真时的tag
        ///  没有顺序和层级，会遍历所有range
        /// </summary>
        public ITagObject FindObjectWithTagInRange(Func<bool,string> tagCharger, IEnumerable<Vector2> rangeArray);
    }
}
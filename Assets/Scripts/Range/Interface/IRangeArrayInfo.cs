using System;
using System.Collections.Generic;
using UnityEngine;
namespace Range.Interface
{
    
    /// <summary>
    /// 范围使用的设计
    ///         1组          2组          3组
    /// 1层     a            b               c
    /// 2层     d            e               f
    /// 3层      g           h               i
    ///
    /// 同一组存在逻辑上的先后关系 (这种先后可以表示距离的顺序)
    /// 不同组的同一层存在顺序关系
    /// </summary>
    public interface IRangeArrayInfo
    {
        public int MaxLayer { get; }
        public int MaxGroup { get; }

        public void Initialize(params string[] args);
        /// <summary>
        /// 获得某一层的所有范围
        /// </summary>
        public IReadOnlyList<Vector2Int> GetLayerRanges(int layer);
        /// <summary>
        /// 获得某一组的所有范围
        /// </summary>
        public IReadOnlyList<Vector2Int> GetGroupRanges(int group);
        /// <summary>
        /// 获得某一层某一组的范围
        /// </summary>
        public IReadOnlyList<Vector2Int> GetRanges(int layer,int group);
        /// <summary>
        /// 获得所有的范围信息
        /// </summary>
        public IReadOnlyList<Vector2Int> GetAllRanges();
    }
}
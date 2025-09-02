using System.Collections.Generic;
using Range.Interface;
using UnityEngine;
namespace Range
{
    /// <summary>
    /// 暂时写死了
    /// </summary>
    public class DefaultRangeInfo : IRangeInfo
    {
        private List<IRangeInfo.Range> ranges;
        
        public void Initialize(params string[] args)
        {
            ranges=new List<IRangeInfo.Range>();
        }
        public IReadOnlyList<Vector2> GetLayerRanges(int layer)
        {
            return null;
        }
        public IReadOnlyList<Vector2> GetGroupRanges(int group)
        {
            return null;
        }
        public IReadOnlyList<Vector2> GetRanges(int layer, int group)
        {
            return null;
        }
        public IReadOnlyList<Vector2> GetAllRanges()
        {
            return null;
        }
    }
}
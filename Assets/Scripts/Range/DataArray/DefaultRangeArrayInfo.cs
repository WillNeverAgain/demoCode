using System.Collections.Generic;
using System.Linq;
using Range.Interface;
using Unity.VisualScripting;
using UnityEngine;
namespace Range
{
    public class DefaultRangeArrayInfo : IRangeArrayInfo
    {
        IEnumerable<Vector2Int> points;
        
        public DefaultRangeArrayInfo(IEnumerable<Vector2Int> points)
        {
            this.points = points;
        }
        public int MaxLayer => 1;
        public int MaxGroup => 1;
        public void Initialize(params string[] args)
        {
            throw new System.NotImplementedException("使用默认的范围组请在构造函数初始化,而不是在Initialize中");
        }
        public IReadOnlyList<Vector2Int> GetLayerRanges(int layer)
        {
            return points.ToList();
        }
        public IReadOnlyList<Vector2Int> GetGroupRanges(int group)
        {
            return points.ToList();
        }
        public IReadOnlyList<Vector2Int> GetRanges(int layer, int group)
        {
            return points.ToList();
        }
        public IReadOnlyList<Vector2Int> GetAllRanges()
        {
            return points.ToList();
        }
    }
}
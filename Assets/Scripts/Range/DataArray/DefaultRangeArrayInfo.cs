using System.Collections.Generic;
using System.Linq;
using Range.Interface;
using Unity.VisualScripting;
using UnityEngine;
namespace Range
{
    public class DefaultRangeArrayInfo : IRangeArrayInfo
    {
        IEnumerable<Vector2> points;
        
        public DefaultRangeArrayInfo(IEnumerable<Vector2> points)
        {
            this.points = points;
        }
        public int MaxLayer => 1;
        public int MaxGroup => 1;
        public void Initialize(params string[] args)
        {
            throw new System.NotImplementedException("使用默认的范围组请在构造函数初始化,而不是在Initialize中");
        }
        public IReadOnlyList<Vector2> GetLayerRanges(int layer)
        {
            return points.ToList();
        }
        public IReadOnlyList<Vector2> GetGroupRanges(int group)
        {
            return points.ToList();
        }
        public IReadOnlyList<Vector2> GetRanges(int layer, int group)
        {
            return points.ToList();
        }
        public IReadOnlyList<Vector2> GetAllRanges()
        {
            return points.ToList();
        }
    }
}
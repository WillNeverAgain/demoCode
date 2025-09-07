using System.Collections.Generic;
using System.Linq;
using Range;
using Range.Interface;
using UnityEngine;
namespace UniTest.Demo1
{
    /// <summary>
    /// 暂时写死了
    /// </summary>
    public class TestRangeArrayInfo : IRangeArrayInfo
    {
        private List<IRangeData> ranges;

        public int MaxLayer => 2;
        public int MaxGroup => 1;
        public IRangeArrayInfo Initialize(params string[] args)
        {
            ranges=new List<IRangeData>();
            ranges.Add(new RangeData()
            {
                layerID = 0,
                groupID = 0,
                offset = Vector2Int.zero,
            });
            ranges.Add(new RangeData()
            {
                layerID = 1,
                groupID = 0,
                offset = Vector2Int.left,
            });
            ranges.Add(new RangeData()
            {
                layerID = 1,
                groupID = 0,
                offset = Vector2Int.right,
            });
            ranges.Add(new RangeData()
            {
                layerID = 1,
                groupID = 0,
                offset = Vector2Int.up,
            });
            ranges.Add(new RangeData()
            {
                layerID = 1,
                groupID = 0,
                offset = Vector2Int.down,
            });
            return this;
        }
        public IReadOnlyList<Vector2Int> GetLayerRanges(int layer)
        {
            return ranges.Where(val=>val.LayerID==layer).Select(te=>te.Offset).ToList();
        }
        public IReadOnlyList<Vector2Int> GetGroupRanges(int group)
        {
            return ranges.Where(val=>val.GroupID==group).Select(te=>te.Offset).ToList();
        }
        public IReadOnlyList<Vector2Int> GetRanges(int layer, int group)
        {
            return ranges.Where(val=>val.GroupID==group && val.LayerID==layer)?.Select(te=>te.Offset)?.ToList();

        }
        public IReadOnlyList<Vector2Int> GetAllRanges()
        {
            return ranges.Select(te=>te.Offset).ToList();
        }
    }
}
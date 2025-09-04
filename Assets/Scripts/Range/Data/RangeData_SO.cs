using UnityEngine;
namespace Range
{
    /// <summary>
    /// 单个范围信息
    /// </summary>
    public class RangeData_SO : ScriptableObject , IRangeData
    {
        /// <summary>
        /// 组号
        /// </summary>
        public int groupID;
        /// <summary>
        /// 层号(总)
        /// </summary>
        public int layerID;

        /// <summary>
        /// 偏移量
        /// </summary>
        public Vector2 offset;
        public int GroupID => groupID;
        public int LayerID => layerID;
        public Vector2 Offset => offset;
    }
}
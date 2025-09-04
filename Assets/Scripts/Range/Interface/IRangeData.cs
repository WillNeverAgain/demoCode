using UnityEngine;
namespace Range
{
    public interface IRangeData
    {
        /// <summary>
        /// 组号
        /// </summary>
        public int GroupID{get;}
        /// <summary>
        /// 层号(总)
        /// </summary>
        public int LayerID{get;}
        /// <summary>
        /// 偏移量
        /// </summary>
        public Vector2 Offset{get;}
    }
    

}
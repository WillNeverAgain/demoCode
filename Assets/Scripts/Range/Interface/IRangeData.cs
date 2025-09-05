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
        /// 偏移量 （Cell Pos）
        /// </summary>
        public Vector2Int Offset{get;}
    }
    

}
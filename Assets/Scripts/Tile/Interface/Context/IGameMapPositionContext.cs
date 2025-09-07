using UnityEngine;
namespace Tile.Base
{
    public interface IGameMapPositionContext  : IMapContext
    {

        public void Initialize(Vector3 wordOffset);

        /// <summary>
        /// 逻辑坐标转换成cell坐标
        /// </summary>
        public Vector2Int LogicPosToCell(Vector2 position);
        /// <summary>
        /// cell坐标转换成逻辑坐标
        /// </summary>
        public Vector2 CellPosToLogic(Vector2Int pos);
        /// <summary>
        /// 逻辑坐标转换成世界坐标
        /// </summary>
        public Vector3 LogicPosToWorld(Vector2 pos);
        /// <summary>
        /// 世界坐标转换成逻辑坐标
        /// </summary>
        public Vector2 WorldPosToLogic(Vector3 pos);

        /// <summary>
        /// cell坐标转换成世界坐标
        /// </summary>
        public Vector3 CellPosToWorld(Vector2Int pos);
        
        /// <summary>
        ///  世界坐标转换成cell坐标
        /// </summary>
        public Vector2Int WorldPosToCell(Vector3 worldPos);
    }
}
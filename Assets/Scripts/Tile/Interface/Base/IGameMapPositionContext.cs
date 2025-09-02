using UnityEngine;
namespace Tile.Base
{
    public interface IGameMapPositionContext
    {

        public void Initialize(Vector3 wordOffset);

        /// <summary>
        /// 逻辑坐标转换成cell坐标
        /// </summary>
        public Vector2Int LogicPosToCell(Vector2Int position);
        public Vector2Int LogicPosToCell(int x,int y);

        /// <summary>
        /// cell坐标转换成逻辑坐标
        /// </summary>
        public Vector2Int CellPosToLogic(Vector2Int pos);
        public Vector2Int CellPosToLogic(int x,int y);
        
        /// <summary>
        /// 逻辑坐标转换成世界坐标
        /// </summary>
        public Vector3 LogicPosToWorld(Vector2Int pos);
        public Vector3 LogicPosToWorld(int x,int y);

        /// <summary>
        /// 世界坐标转换成逻辑坐标
        /// </summary>
        public Vector2Int WorldPosToLogic(Vector3 pos);

        /// <summary>
        /// cell坐标转换成世界坐标
        /// </summary>
        public Vector3 CellPosToWorld(Vector2Int pos);
        public Vector3 CellPosToWorld(int x,int y);
        
        /// <summary>
        ///  世界坐标转换成cell坐标
        /// </summary>
        public Vector2Int WorldPosToCell(Vector3 worldPos);
    }
}
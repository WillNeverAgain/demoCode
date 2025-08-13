using UnityEngine;
namespace Tile.Context
{
    
    
    /// <summary>
    /// 地图的逻辑位置应该符合下面公式
    ///     ....
    ///     0,2   1,2   2,2   3,2
    ///     0,1   1,1   2,1   3,1    
    ///     0,0   1,0   2,0   3,0 ... Wide
    /// </summary>
    public class FourGridTilePositionContext : IGameMapPositionContext
    {
        private Vector3 _positionOffset=Vector3.zero;
        private Vector2Int _logicalOffset;
        /// <param name="offset">偏移量</param>
        public void Initialize(Vector3 wordOffset)
        {
            //避免重复初始化
            if(_positionOffset.Equals(wordOffset))return;
            _positionOffset = wordOffset;
            //这里直接用临近的位置做逻辑0点了
            _logicalOffset = new Vector2Int((int)wordOffset.x, (int)wordOffset.y);
        }

        public Vector2Int LogicPosToCell(int x, int y)
        {
            return new Vector2Int(x-_logicalOffset.x, y-_logicalOffset.y);
        }

        public Vector2Int CellPosToLogic(int x, int y)
        {
            return new Vector2Int(x+_logicalOffset.x, y+ _logicalOffset.y);
        }

        public Vector3 LogicPosToWorld(int x, int y)
        {
            return new Vector3(x + _logicalOffset.x+0.5f, y + _logicalOffset.y+0.5f, 0);
        }
        public Vector2Int WorldPosToLogic(Vector3 pos)
        {
           return new Vector2Int((int)pos.x, (int)pos.y);
        }
        /// <summary>
        /// 默认在中间
        /// </summary>
        public Vector3 CellPosToWorld(Vector2Int pos)
        {
            return CellPosToWorld(pos.x, pos.y);
        }
        public Vector3 CellPosToWorld(int x, int y)
        {
            return LogicPosToWorld(CellPosToLogic(x,y));
        }
        public Vector2Int WorldPosToCell(Vector3 worldPos)
        {
           return LogicPosToCell( WorldPosToLogic(worldPos));
        }
        
        public Vector2Int LogicPosToCell(Vector2Int position)
        {
            return LogicPosToCell(position.x, position.y);
        }
        
        public Vector2Int CellPosToLogic(Vector2Int pos)
        {
            return CellPosToLogic(pos.x, pos.y);
        }
        
        public Vector3 LogicPosToWorld(Vector2Int pos)
        {
            return LogicPosToWorld(pos.x, pos.y);
        }
    }
}
using Tile.Base;
using UnityEngine;
namespace Tile.Context
{
    /// <summary>
    /// 基于unity组件的上下文
    /// </summary>
    public class UnityGridTilePositionContext : IGameMapPositionContext
    {
        public UnityGridTilePositionContext(Grid grid)
        {
            this.grid = grid;
            this.xLength = grid.cellSize.x;
            this.yLength = grid.cellSize.y;
        }
        private Grid grid;
        private Vector3 wOffset;
        private Vector2Int logicalOffset;
        private float xLength,yLength;
        public void Initialize(Vector3 wordOffset)
        {
            wOffset = wordOffset;
            logicalOffset=Vector2Int.FloorToInt(wordOffset);
        }
        public Vector2Int LogicPosToCell(Vector2Int position)
        {
            return position-logicalOffset;
        }
        public Vector2Int LogicPosToCell(int x, int y)
        {
            return LogicPosToCell(new Vector2Int(x,y));
        }
        public Vector2Int CellPosToLogic(Vector2Int pos)
        {
            return pos+logicalOffset;
        }
        public Vector2Int CellPosToLogic(int x, int y)
        {
            return CellPosToLogic(new Vector2Int(x, y));
        }
        public Vector3 LogicPosToWorld(Vector2Int pos)
        {
            return new Vector3(pos.x+xLength/2f,pos.y+yLength/2f,0);
        }
        public Vector3 LogicPosToWorld(int x, int y)
        {
            return LogicPosToWorld(new Vector2Int(x, y));
        }
        public Vector2Int WorldPosToLogic(Vector3 pos)
        {
            Vector3Int temp = grid.WorldToCell(pos);
            return new Vector2Int( temp.x,temp.y);
        }
        public Vector3 CellPosToWorld(Vector2Int pos)
        {
            return LogicPosToWorld(CellPosToLogic(pos));
        }
        public Vector3 CellPosToWorld(int x, int y)
        {
            return LogicPosToWorld(CellPosToLogic(new Vector2Int(x,y)));
        }
        public Vector2Int WorldPosToCell(Vector3 worldPos)
        {
            return LogicPosToCell(WorldPosToLogic(worldPos));
        }
    }
}
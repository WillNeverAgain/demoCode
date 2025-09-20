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
        private Vector2 logicalOffset;
        private Vector2Int cellOffset;
        private float xLength,yLength;
        private Vector3 tempWorldOffset;
        public void Initialize(Vector3 wordOffset)
        {
            logicalOffset=wordOffset;
            cellOffset=new Vector2Int( grid.WorldToCell(wordOffset).x,grid.WorldToCell(wordOffset).y);
            tempWorldOffset=new Vector3(wordOffset.x,wordOffset.y,0);
        }
        public Vector2Int LogicPosToCell(Vector2 position)
        {
            return new Vector2Int(Mathf.FloorToInt(position.x),Mathf.FloorToInt(position.y/yLength));
        }
        public Vector3 LogicPosToWorld(Vector2 pos)
        {
            return  new Vector3(pos.x,pos.y)+tempWorldOffset ;
        }
        public Vector2 CellPosToLogic(Vector2Int pos)
        {
            return grid.CellToLocal(new Vector3Int(pos.x,pos.y,0));
        }
        public Vector2 WorldPosToLogic(Vector3 pos)
        {
            Vector3Int temp = grid.WorldToCell(pos);
            return new Vector2Int( temp.x,temp.y);
        }
        public Vector3 CellPosToWorld(Vector2Int pos)
        {
            return LogicPosToWorld(CellPosToLogic(pos));
        }
        public Vector2Int WorldPosToCell(Vector3 worldPos)
        {
            Vector3Int temp = grid.WorldToCell(worldPos);
            return new Vector2Int( temp.x,temp.y) - cellOffset;
        }
    }
}
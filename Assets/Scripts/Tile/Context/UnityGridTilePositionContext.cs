using System;
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
        private Vector2 logicalOffset;
        private float xLength,yLength;
        public void Initialize(Vector3 wordOffset)
        {
            wOffset = wordOffset;
            logicalOffset=wordOffset;
        }
        public Vector2Int LogicPosToCell(Vector2 position)
        {
            //TODO: 
            return Vector2Int.zero;
        }
        public Vector2Int LogicPosToCell(int x, int y)
        {
            return LogicPosToCell(new Vector2Int(x,y));
        }
        public Vector3 LogicPosToWorld(Vector2 pos)
        {
            throw new NotImplementedException();
        }
        public Vector2 CellPosToLogic(Vector2Int pos)
        {
            return TransCellPosToLogical(pos)+logicalOffset;
        }
        private Vector2 TransCellPosToLogical(Vector2Int ps)
        {
            Vector2 res = new Vector2();
            res.x = MathF.Abs( ps.x/2f)<1? MathF.Sign(ps.x)*xLength/2 : MathF.Floor( ps.x*  xLength)  + MathF.Sign(ps.x)*xLength/2;
            res.y = ps.y%2==0?  (int)ps.y*yLength/2 : (int)ps.y*yLength/2;
            //TODO: 
            return Vector2.zero;
        }
        public Vector3 LogicPosToWorld(Vector2Int pos)
        {
            return new Vector3(pos.x,pos.y,0);
        }
        Vector2 IGameMapPositionContext.WorldPosToLogic(Vector3 pos)
        {
            return WorldPosToLogic(pos);
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
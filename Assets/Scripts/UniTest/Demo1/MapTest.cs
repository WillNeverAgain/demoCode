using System.Text;
using Tile;
using Tile.Base;
using Tile.Context;
using UnityEngine;
namespace UniTest.Demo1
{
    [UniTest]
    public static class MapTest
    {
        private static IGameMapPositionContext _context = new RhombusGridTilePositionContext();
        private class WorldTestData
        {
            Vector3 position;
            private Vector2Int cellPos;
            private Vector2Int logicalPos;
            private Vector2Int cellToLogicalPos;
            private Vector3 cellToWorldPos;

            public WorldTestData()
            {
                position=new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
                cellPos= _context.WorldPosToCell(position);
                logicalPos= _context.WorldPosToLogic(position);
                cellToLogicalPos = _context.CellPosToLogic(cellPos);
                cellToWorldPos= _context.CellPosToWorld(cellPos);
            }
            public override string ToString()
            {
                return $"  Position: {position}  | LogicalPos: {logicalPos}   |  CellPos: {cellPos}    | \n CellToWorldPos: {cellToWorldPos}  |  CellToLogicalPos: {cellToLogicalPos}";
            }
        }
        public static void TestPosContext()
        {
            _context.Initialize(Vector3.zero);
            StringBuilder builder=new StringBuilder();
            for (int i = 0; i < 30; i++)
            {
                builder.Append(new WorldTestData().ToString());
                builder.Append("\n");
            }
            Debug.Log(builder.ToString());
            
            
            _context.Initialize(Vector3.one);
            builder.Clear();
            for (int i = 0; i < 30; i++)
            {
                builder.Append(new WorldTestData().ToString());
                builder.Append("\n");
            }
            Debug.Log(builder.ToString());
            
            _context.Initialize(new Vector3(1.1f, -1.1f, 2.1f));
            builder.Clear();
            for (int i = 0; i < 30; i++)
            {
                builder.Append(new WorldTestData().ToString());
                builder.Append("\n");
            }
            Debug.Log(builder.ToString());
            
                        
            _context.Initialize(new Vector3(0, -3, 1));
            builder.Clear();
            for (int i = 0; i < 30; i++)
            {
                builder.Append(new WorldTestData().ToString());
                builder.Append("\n");
            }
            Debug.Log(builder.ToString());
        }
    }
}
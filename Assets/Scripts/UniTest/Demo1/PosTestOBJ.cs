using Tile;
using Tile.Context;
namespace UniTest.Demo1
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PosTestOBJ : MonoBehaviour
    {
        private static IGameMapPositionContext _context = new RhombusGridTilePositionContext();
        // Start is called before the first frame update
        void Start()
        {
            _context.Initialize(new Vector3(1,0,0));
        }
        private class WorldTestData
        {
            Vector3 position;
            private Vector2Int cellPos;
            private Vector2Int logicalPos;
            private Vector2Int cellToLogicalPos;
            private Vector3 cellToWorldPos;

            public WorldTestData(Vector3 ps)
            {
                position = ps;
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
        private float timer = 0;
        // Update is called once per frame
        void Update()
        {
            if (timer <= 0)
            {
                Debug.Log(   new WorldTestData(transform.position).ToString());
                timer = 0.5f;
            }
            timer -= Time.deltaTime;
        }
    }

}
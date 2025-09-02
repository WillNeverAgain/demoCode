using Tile;
using Tile.Base;
using Tile.Context;
namespace UniTest.Demo1
{
    using UnityEngine;

    public class PosTestOBJ : MonoBehaviour
    {
        private static IGameMapPositionContext _context ;
        [SerializeField] private Grid _grid;
        // Start is called before the first frame update
        void Start()
        {
            _context= new UnityGridTilePositionContext(_grid);
            _context.Initialize(new Vector3(1,0,0));
        }
        private class WorldTestData
        {
            Vector3 position;
            private Vector2Int cellPos;
            private Vector2 logicalPos;
            private Vector2 cellToLogicalPos;
            private Vector3 cellToWorldPos;

            public WorldTestData(Vector3 ps)
            {
                position = ps;
                logicalPos= _context.WorldPosToLogic(position);
                cellToLogicalPos = _context.CellPosToLogic(cellPos);
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
using System;
using System.Collections.Generic;
using System.Text;
using Range;
using Range.Interface;
using Tile;
using Tile.Base;
using Tile.Context;
using Tile.Factory;
using Tile.Middleware;
using Tile.Middleware.Factory;
using Tile.Model;
using Tile.TileTags;
using Tile.View;
using UnityEngine;
namespace UniTest.Demo1
{
    public class Demo1MonoTileSystem : MonoBehaviour
    {
        private  ITileMapSystem _tileMapSystem;
        [Header("Tile 系统设置")]
        [SerializeField]
        private Grid _grid;
        [SerializeField]
        private GameObject _tilePrefab;
        private IMapCellRangeMiddleware _middleware;
        [SerializeField] private bool posDebug;

        [Range(0,4)] [SerializeField] private int rangeMask;
        
        
        private List<Vector2Int> debugPoints ;
        private List<Vector2Int> mousePoints ;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _tileMapSystem = new UnityGridTileMapSystem();
            _tileMapSystem.Initialize(_grid, _tilePrefab);
            _middleware = _tileMapSystem.GetMiddleware<IMapCellRangeMiddleware>();
            debugPoints= new List<Vector2Int>();
            mousePoints = new List<Vector2Int>();
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    debugPoints.Add(new Vector2Int(i, j));
                }
            }
            
        }   
        private void Update()
        {
            _tileMapSystem.Update(Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                IRangeArrayInfo info =null;
                switch (rangeMask)
                {
                    case 0:
                        info = new TestRangeArrayInfo().Initialize();
                        break;
                    case 1:
                        info = new DefaultRangeArrayInfo(new Vector2Int[]
                        {
                            new Vector2Int(0, 0),new Vector2Int(1, 0),new Vector2Int(0, 1),
                            new Vector2Int(2, 0),new Vector2Int(1, 1),new Vector2Int(2, 1)
                        });
                        break;
                    case 2:
                        info = new DefaultRangeArrayInfo(new Vector2Int[]
                        {
                            new Vector2Int(2, 0),new Vector2Int(-2, 0),
                            new Vector2Int(0, 2),new Vector2Int(0, -2)
                        });
                        break;
                    case 3:
                        info = new DefaultRangeArrayInfo(new Vector2Int[]
                        {
                            new Vector2Int(2, 0),new Vector2Int(-2, 0),
                            new Vector2Int(0, 2),new Vector2Int(0, -2),
                            new Vector2Int(1, 1),new Vector2Int(-1, 1),
                            new Vector2Int(-1, -1),new Vector2Int(1, -1),
                        });
                        break;
                    case 4:
                        info = new DefaultRangeArrayInfo(new Vector2Int[]
                        {
                            new Vector2Int(2, 0),new Vector2Int(-2, 0),
                            new Vector2Int(0, 2),new Vector2Int(0, -2),
                            new Vector2Int(1, 1),new Vector2Int(-1, 1),
                            new Vector2Int(-1, -1),new Vector2Int(1, -1),
                            new Vector2Int(4, 0),new Vector2Int(-4, 0),
                            new Vector2Int(0, 4),new Vector2Int(0, -4),
                            new Vector2Int(2, 2),new Vector2Int(-2, 2),
                            new Vector2Int(-2, -2),new Vector2Int(2, -2),
                        });
                        break;
                }
                
                mousePoints.Clear();
                var ps = Camera.main.ScreenToWorldPoint( Input.mousePosition);
                ps.z = 0;
                Debug.Log(ps);
                var res= _middleware.GetCellWithTagInRange(ps, (a) => a.Contains(CellMoveTags.MOVE_ABLE_CELL), 
                    info);
                foreach (var c in res)
                {
                    mousePoints.Add(c.CellPosition);
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (!posDebug)return;
            IMapPositionTransMiddleware posMid = _tileMapSystem.GetMiddleware<IMapPositionTransMiddleware>();
            if (mousePoints.Count > 0)
            {
                foreach (var c in mousePoints)
                {
                    var resPos= posMid.CellPosToWorld(c);
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(resPos, 0.3f);
                }
            }
           var res=  _middleware.GetCellWithTagInRange(Vector3.zero, (a) => a.Contains(CellMoveTags.MOVE_ABLE_CELL), 
               new DefaultRangeArrayInfo(debugPoints));
           foreach (var c in res)
           {
               if (mousePoints.Contains(c.CellPosition)) continue;
               var resPos= posMid.CellPosToWorld(c.CellPosition);
               Gizmos.color = Color.white;
               Gizmos.DrawWireSphere(resPos, 0.3f);
           }
        }
    }
}
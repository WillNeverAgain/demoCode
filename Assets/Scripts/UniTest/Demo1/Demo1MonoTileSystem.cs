using System;
using System.Text;
using Range;
using Tile;
using Tile.Base;
using Tile.Context;
using Tile.Factory;
using Tile.Middleware;
using Tile.Middleware.Factory;
using Tile.Model;
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
        private ICellRangeMapMiddleware _mapMiddleware;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _tileMapSystem = new UnityGridTileMapSystem();
            _tileMapSystem.Initialize(_grid, _tilePrefab);
            _mapMiddleware = _tileMapSystem.GetMiddleware<ICellRangeMapMiddleware>();
        }   
        private void Update()
        {
            _tileMapSystem.Update(Time.deltaTime);
            if (Input.GetMouseButtonDown(0))
            {
                var ps = Camera.main.ScreenToWorldPoint( Input.mousePosition);
                var res= _mapMiddleware.GetCellWithTagInRange(ps, (a) => true, new TestRangeArrayInfo().Initialize());
                StringBuilder temp=new StringBuilder();
                foreach (var c in res)
                {
                    temp.Append( $" CellPos :  {c.CellPosition} \n") ;
                }
                Debug.Log(temp.ToString());
            }
        }
    }
}
using System.Collections.Generic;
using Tile.Base;
using Tile.Interface.Base;
using Tile.ObjectContainer;
using Tile.SO;
using Tile.TileTags;
using UnityEngine;
namespace Tile.TillCell.Demo1
{
    /// <summary>
    /// TODO: 硬编码，后期修改
    /// </summary>
    public class GroundCell : IGameTileCell
    {
        private static ICreateCellInfo _CellInfo;
        public IList<string> Tags => tags;
        private List<string> tags=new List<string>()
        {
            CellMoveTags.MOVE_ABLE_CELL
        };
        GameObject gameObject;
        void IGameTileCell.Render(int x, int y, IGameMapRefreshContext refreshContext)
        {
            if (gameObject == null)
            {
                CreateCell(x,y,refreshContext);
            }
        }
        public Vector2Int CellPosition =>cellPosition;
        public IMapObjectContainer Container => _container;
        private IMapObjectContainer _container;
        private Vector2Int cellPosition=Vector2Int.zero;

        private void OnInit()
        {
            _CellInfo = Resources.Load<TileCellModel_SO>("Demo1/SOs/GroundCell");
        }
        private void CreateCell(int x, int y, IGameMapRefreshContext refreshContext)
        {
            if (_CellInfo == null)
            {
                OnInit();
            }
            _container = new DefaultObjectContainer();
            gameObject= refreshContext.CellFactory.CreateCell(_CellInfo);
            cellPosition = new Vector2Int(x, y);
            gameObject.transform.position = refreshContext.PositionContext.CellPosToWorld(cellPosition);
            gameObject.SetActive(true);
        }

    }
}
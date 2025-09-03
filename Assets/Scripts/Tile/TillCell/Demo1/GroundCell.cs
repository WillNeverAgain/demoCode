using System.Collections.Generic;
using Tile.Base;
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
        private static CreateCellInfo_SO _CellInfo;
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
        private void OnInit()
        {
            _CellInfo = Resources.Load<CreateCellInfo_SO>("Demo1/SOs/GroundCell");
        }
        private void CreateCell(int x, int y, IGameMapRefreshContext refreshContext)
        {
            if (_CellInfo == null)
            {
                OnInit();
            }
            gameObject= refreshContext.CellFactory.CreateCell(_CellInfo);
            gameObject.transform.position = refreshContext.PositionContext.CellPosToWorld(new Vector2Int(x, y));
            gameObject.SetActive(true);
        }
        public IList<IGameTileObject> GameTileObject => objects;
        private List<IGameTileObject> objects = new List<IGameTileObject>();
    }
}
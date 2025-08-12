using System.Collections.Generic;
using Tile.TileTags;
using UnityEngine;
namespace Tile.TillCell.Demo1
{
    public class GroundCell : IGameTileCell
    {
        public IList<string> Tags => tags;
        private List<string> tags=new List<string>()
        {
            CellMoveTags.MOVE_ABLE_CELL
        };
        GameObject gameObject;
        void IGameTileCell.Render(int x, int y, IGameMapRefreshContext refreshContext)
        {
            //TODO CreateInfo修饰
            gameObject= refreshContext.CellFactory.CreateCell(new CreateCellInfo());
            gameObject.transform.position = refreshContext.PositionContext.CellPosToWorld(x,y);
            gameObject.SetActive(true);
        }
        public IList<IGameTileObject> GameTileObject => objects;
        private List<IGameTileObject> objects = new List<IGameTileObject>();

    }
}
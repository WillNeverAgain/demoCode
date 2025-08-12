using System.Collections.Generic;
using UnityEngine;
namespace Tile.TillCell.Demo1
{
    /// <summary>
    /// 无法移动到的黑暗领域(?
    /// </summary>
    public class DarkCell : IGameTileCell
    {

        public IList<string> Tags {
            get;
        }

        void IGameTileCell.Render(int x, int y, IGameMapRefreshContext tileObject)
        {
           //TODO
        }
        public IList<IGameTileObject> GameTileObject => 
            throw new System.NotSupportedException("尝试获取无法放置物体的cell的物体");
    }
}
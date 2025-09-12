using System.Collections.Generic;
namespace Tile.Data
{
    /// <summary>
    /// 地图数据接口，所有的地图数据
    /// </summary>
    public interface IGameMapData
    {
        public string MapName { get;  }
        public List<List<IGameMapCellData>> MapModel{ get; }
    }
}
using System.Collections.Generic;
using Tile.Interface.Base;
namespace Tile.Data
{
    /// <summary>
    /// 地图数据接口，所有的地图数据
    /// </summary>
    public interface IGameMapData
    {
        string MapName { get; }
        List<List<IGameMapCellData>> MapModel { get; }
    }
}
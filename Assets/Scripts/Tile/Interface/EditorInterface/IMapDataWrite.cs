using System.Collections.Generic;
using UnityEngine;
namespace Tile.EditorInterface
{
    /// <summary>
    /// 可写入的地图数据
    /// </summary>
    public interface IMapDataWrite
    {
        public IGameMapCellData CreateCell(Vector2Int position);
        public IEnumerable<IGameMapCellData> CreateCellEnumerator(IEnumerable<Vector2Int> ranges);
        public IGameMapCellData GetCell(Vector2Int position);
        public IEnumerable<IGameMapCellData> GetCellEnumerator(IEnumerable<Vector2Int> ranges);
        public IGameMapData CreateMapAssets(string mapName);
        public void SaveMapAssets();
        public void LoadMapAssets(string mapName);
    }
}
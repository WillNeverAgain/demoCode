using System.Collections.Generic;
using Tile.EditorInterface;
using Tile.SO;
using UnityEditor;
using UnityEngine;
namespace Tile.Data
{
    public class SOMapDataWriter :  IMapDataWrite
    {
         private string mapName;
        private MapData_SO tempGameMapData;
        private MapCellData_SO CellFactory(int x, int y)
        {
            MapCellData_SO so= ScriptableObject.CreateInstance<MapCellData_SO>() ;
            so.col = x;
            so.row = y;
            so.cellName = mapName;
            return so;
        }
        public IGameMapCellData CreateCell(Vector2Int position)
        {
              List<List<MapCellData_SO>>  list = tempGameMapData.mapData;
              while (list.Count <= position.x)
              {
                  list.Add(new List<MapCellData_SO>());
              }
              while (list[position.x].Count <= position.y)
              {
                  list[position.x].Add(null);
              }
              list[position.x][position.y] =CellFactory(position.x, position.y);
              return list[position.x][position.y];
        }
        public IEnumerable<IGameMapCellData> CreateCellEnumerable(IEnumerable<Vector2Int> ranges)
        {
            List<IGameMapCellData> list = new List<IGameMapCellData>();
            foreach (var pos in ranges)
            {
                list.Add( CreateCell(pos));
            }
            return list;
        }
        public IGameMapCellData GetCell(Vector2Int position)
        {
            if (tempGameMapData.MapModel.Count>position.x && tempGameMapData.MapModel[position.x].Count>position.y)
                return tempGameMapData.MapModel[position.x][position.y];
            return IGameMapCellData.NULL_DATA;
        }
        public IEnumerable<IGameMapCellData> GetCellEnumerable(IEnumerable<Vector2Int> ranges)
        {
            List<IGameMapCellData>data = new List<IGameMapCellData>();
            foreach (var pos in ranges)
            {
                data.Add(GetCell(pos));
            }
            return data;
        }
        public IGameMapData CreateMapAssets(string mapName)
        {
            var map= ScriptableObject.CreateInstance<MapData_SO>();
            map.mapName = mapName;
            this.mapName = mapName;
            map.mapData = new List<List<MapCellData_SO>>();
            // 将SO保存为一个资源文件
            tempGameMapData = map;
            AssetDatabase.CreateAsset(tempGameMapData, $"Assets/Resources/MapData/{mapName}.asset");
            return map;
        }
        public void SaveMapAssets()
        {
            // 保存所有更改的资源
            AssetDatabase.SaveAssetIfDirty(tempGameMapData);
            // 刷新AssetDatabase
            AssetDatabase.Refresh();
        }
    }
}
using System.Collections.Generic;
using Tile.EditorInterface;
using Tile.SO;
using UnityEditor;
using UnityEngine;
namespace Tile.MapEditor.ScenceEditor
{
    public class SceneDataWriter : MonoBehaviour , IMapDataWrite
    {
        private string mapName;
        private IGameMapData tempGameMapData;
        private IGameMapCellData CellFactory(int x, int y)
        {
            return new MapCellData_SO{ row = y,col = x} ;
        }
        public IGameMapCellData CreateCell(Vector2Int position)
        {
              List<List<IGameMapCellData>>  list = tempGameMapData.MapModel;
              while (list.Count <= position.x)
              {
                  list.Add(new List<IGameMapCellData>());
              }
              while (list[position.x].Count <= position.y)
              {
                  list[position.x].Add(IGameMapCellData.NULL_DATA);
              }
              list[position.y][position.x] =CellFactory(position.x, position.y);
              return list[position.x][position.y];
        }
        public IEnumerable<IGameMapCellData> CreateCellEnumerator(IEnumerable<Vector2Int> ranges)
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
        public IEnumerable<IGameMapCellData> GetCellEnumerator(IEnumerable<Vector2Int> ranges)
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
            map.mapData = new List<List<IGameMapCellData>>();
            // 将SO保存为一个资源文件
            AssetDatabase.CreateAsset(map, $"Assets/Data/Map/{mapName}.asset");
            tempGameMapData = map;
            return map;
        }
        public void SaveMapAssets()
        {
            // 保存所有更改的资源
            AssetDatabase.SaveAssets();
            // 刷新AssetDatabase
            AssetDatabase.Refresh();
        }
        public void LoadMapAssets(string mapName)
        {
           var res= AssetBundle.LoadFromFile($"Assets/Data/Map/{mapName}.asset");
           if (res == null)
           {
               Debug.LogError("Map Assets not found");
           }
           else
           {
               tempGameMapData=res as IGameMapData;
               RefreshScene();
           }
        }
        private void RefreshScene()
        {
            
        }
    }
}
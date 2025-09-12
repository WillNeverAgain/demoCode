using System.Collections.Generic;
using Tile.Base;
using Tile.Data;
using Tile.Model;
using Tile.SO;
using UnityEngine;
namespace Tile.ResourcesLoader
{
    // TODO:? 需要统一管理吗
    public class SOMapResourcesLoader : IMapResourcesLouder
    {
        private const string tempSOPath = "MapSO";
        private Dictionary<string,IGameMapData> maps;
        private SOMapResourcesLoader()
        {
            maps = new Dictionary<string, IGameMapData>();
           var res= Resources.LoadAll<GameMapModelData_SO>(tempSOPath);
           foreach (var data in res)
           {
               maps.Add(data.name, data);
           }
        }
        public IGameMapData GetMapResources(string mapName)
        {
            return maps[mapName];
        }
    }
}
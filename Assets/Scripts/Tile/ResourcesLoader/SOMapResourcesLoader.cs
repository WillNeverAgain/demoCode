using System.Collections.Generic;
using Tile.Data;
using UnityEngine;
namespace Tile.ResourcesLoader
{
    public class SOMapResourcesLoader : IMapResourcesLouder
    {
        private const string tempSOPath = "MapData";
        private Dictionary<string,IGameMapData> maps;
        private SOMapResourcesLoader()
        {
            maps = new Dictionary<string, IGameMapData>();
           var res= Resources.LoadAll<MapData_SO>(tempSOPath);
           foreach (var so in res)
           {
               maps.Add(so.MapName, so);
           }
        }
        public IGameMapData GetMapResources(string mapName)
        {
            return maps[mapName];
        }
    }
}
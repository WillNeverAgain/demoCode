using System;
using System.Collections.Generic;
using System.Linq;
using Tile.Data;
using UnityEngine;
namespace Tile.SO
{
    [Serializable]
    public class SOMapData_List
    {
        public List<TileCellModel_SO> mapData = new List<TileCellModel_SO>();
    }
    [CreateAssetMenu(fileName = "SO/MapData_SO", menuName = "SO/MapData_SO")]
    public class MapData_SO : ScriptableObject ,IGameMapData
    {
        public string mapName;
        public List<SOMapData_List> mapData;
        public string MapName => mapName;
        public List<List<IGameMapCellData>> MapModel => (List<List<IGameMapCellData>>)mapData.Select(tL=>tL.mapData);
    }
}
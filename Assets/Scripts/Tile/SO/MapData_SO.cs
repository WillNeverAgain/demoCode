using System;
using System.Collections.Generic;
using Tile.Data;
using Tile.Interface.Base;
using Tile.SO;
using UnityEngine;
[CreateAssetMenu(fileName = "MapData_SO", menuName = "SO/MapData_SO")]
public class MapData_SO : ScriptableObject, IGameMapData
{
    public string mapName;
    public List<List<MapCellData_SO>> mapData = new List<List<MapCellData_SO>>();

    public string MapName => mapName;
    public List<List<IGameMapCellData>> MapModel
    {
        get
        {
            var model = new List<List<IGameMapCellData>>();
            foreach (var row in mapData)
            {
                var rowList = new List<IGameMapCellData>();
                foreach (var cell in row)
                {
                    rowList.Add(cell);
                }
                model.Add(rowList);
            }
            return model;
        }
    }
}
using System;
using System.Collections.Generic;
using Tile.SO;
using UnityEngine;

// 单元格数据接口，确保扩展性
public interface IGameMapCellData
{
    public const IGameMapCellData NULL_DATA = null;
    public string CellID { get; }
    //TODO : 组件
}
public interface IGameMapData
{
    string MapName { get; }
    List<List<IGameMapCellData>> MapModel { get; }
}

[CreateAssetMenu(fileName = "MapData_SO", menuName = "SO/MapData_SO")]
public class MapData_SO : ScriptableObject, IGameMapData
{
    public string mapName;
    public List<List<IGameMapCellData>> mapData = new List<List<IGameMapCellData>>();

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
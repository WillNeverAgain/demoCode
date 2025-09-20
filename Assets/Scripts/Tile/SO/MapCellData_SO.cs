using System;
using System.Collections.Generic;
using Tile.Data;
using Tile.Interface.Base;
using UnityEngine;
namespace Tile.SO
{
    public class MapCellData_SO : ScriptableObject, IGameMapCellData
    {
        // 添加您的单元格属性
        public int row;
        public int col;
        public string cellName;
        public Sprite sprite;
        public List<string> component;
        public int layer;
        public string CellID => cellName;
    }
}
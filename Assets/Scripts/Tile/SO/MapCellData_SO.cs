using System;
using System.Collections.Generic;
using UnityEngine;
namespace Tile.SO
{
    public class MapCellData_SO : ScriptableObject, IGameMapCellData
    {
        // 添加您的单元格属性
        public int row;
        public int col;
        public string cellName;
        public List<string> component;
        public string CellID => cellName;
    }
}
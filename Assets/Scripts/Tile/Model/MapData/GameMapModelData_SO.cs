using System.Collections.Generic;
using Tile.Data;
using UnityEngine;
namespace Tile.SO
{
    public class GameMapModelData_SO : ScriptableObject , IGameMapData
    {

        public string MapName {
            get;
        }
        public List<List<IGameMapCellData>> MapModel {
            get;
        }
    }
}
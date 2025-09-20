using System.Collections.Generic;
using Tile.Interface.Base;
using UnityEngine;
using UnityEngine.Serialization;
namespace Tile.SO
{
    
    [CreateAssetMenu(fileName = "TileCellModel_SO", menuName = "SO/TileCellModel_SO")]
    public class TileCellModel_SO : ScriptableObject ,ICreateCellInfo
    {
       public Sprite dataSprite;
        public string dataCellName;
        public int dataTileLayer;
        /// <summary>
        /// 初始化的args
        /// </summary>
        public List<object> dataArgs;
        public Sprite Sprite => dataSprite;
        public string CellName => dataCellName;
        public int TileLayer => dataTileLayer;
        public IReadOnlyList<object> args => dataArgs;
    }
}
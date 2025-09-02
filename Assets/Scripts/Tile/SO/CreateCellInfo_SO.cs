using System.Collections.Generic;
using Tile.Interface.Base;
using UnityEngine;
using UnityEngine.Serialization;
namespace Tile.SO
{

    
    //TODO: 通用的通过模板创建cell的信息
    //实际上是数据的一种demo1暂时用so 后面换成json
    // 暂时用硬编码实现了
    [CreateAssetMenu(fileName = "CreateCellInfo_SO", menuName = "SO/CreateCellInfo_SO")]
    public class CreateCellInfo_SO : ScriptableObject ,ICreateCellInfo
    {
       public Sprite dataSprite;
        public string dataCellName;
        public int dataTileLayer;
        /// <summary>
        /// 初始化的args
        /// </summary>
        public List<string> dataArgs;
        public Sprite Sprite => dataSprite;
        public string CellName => dataCellName;
        public int TileLayer => dataTileLayer;
        public IReadOnlyList<string> args => dataArgs;
    }
}
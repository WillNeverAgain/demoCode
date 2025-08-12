using UnityEngine;
namespace Tile
{
    public interface ICellFactory
    {
        /// <summary>
        /// 创建Cell的工厂
        /// </summary>
        public GameObject CreateCell(CreateCellInfo info);
    }
    //TODO: 通用的通过模板创建cell的信息
    public struct CreateCellInfo
    {
        
    }
}
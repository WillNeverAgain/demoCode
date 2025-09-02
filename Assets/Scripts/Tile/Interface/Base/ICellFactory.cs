using UnityEngine;
namespace Tile.Base
{
    public interface ICellFactory 
    {
        /// <summary>
        /// 预留通用接口
        /// </summary>
        public void Initialize(params object[] args);
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
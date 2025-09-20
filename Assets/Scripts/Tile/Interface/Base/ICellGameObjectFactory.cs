using System.Collections.Generic;
using Tile.Interface.Base;
using Tile.SO;
using UnityEngine;
namespace Tile.Base
{
    
    public interface ICellGameObjectFactory 
    {
        /// <summary>
        /// 预留通用接口
        /// </summary>
        public void Initialize(params object[] args);
        /// <summary>
        /// 创建Cell的工厂
        /// </summary>
        public GameObject CreateCell(ICreateCellInfo infoSo);
    }

}
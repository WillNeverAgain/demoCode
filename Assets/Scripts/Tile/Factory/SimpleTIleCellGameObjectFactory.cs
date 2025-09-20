using Tile.Base;
using Tile.Interface.Base;
using Tile.SO;
using UnityEngine;
namespace Tile.Factory
{
    /// <summary>
    /// 临时工厂
    /// 暂时还每决定工厂的数据
    /// </summary>
    public class SimpleTIleCellGameObjectFactory : ICellGameObjectFactory
    {
        private GameObject GroundCellPrefab = null;
        public void Initialize(params object[] args)
        {
            AssetBundle.LoadFromFile("");
        }
        public GameObject CreateCell(ICreateCellInfo infoSo)
        {
            return null;
        }
    }
}
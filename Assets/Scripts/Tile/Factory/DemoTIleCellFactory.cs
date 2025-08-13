using UnityEngine;
namespace Tile.Factory
{
    /// <summary>
    /// 临时工厂
    /// 暂时还每决定工厂的数据
    /// </summary>
    public class DemoTIleCellFactory : ICellFactory
    {
        private GameObject GroundCellPrefab = null;
        public void Initialize(params object[] args)
        {
            AssetBundle.LoadFromFile("");
        }
        public GameObject CreateCell(CreateCellInfo info)
        {
            return null;
        }
    }
}
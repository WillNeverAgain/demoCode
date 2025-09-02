using System.Collections.Generic;
using Tile.Base;
using Tile.Interface.Base;
using Tile.SO;
using Tile.TillCell;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Tile.Factory
{
    public class UnityGridCellFactory : ICellFactory
    {
        private Transform gridTransform;
        private GameObject tilePrefab;
        /// <param name="args">
        /// @arg0 gridTransform \\
        ///  @arg1 tilePrefab \\
        /// </param>
        public void Initialize(params object[] args)
        {
            gridTransform = args[0] as Transform;
            tilePrefab = args[1] as GameObject;
        }
        public GameObject CreateCell(ICreateCellInfo infoSo)
        {
            GameObject temp = GameObject.Instantiate(tilePrefab,gridTransform);
            temp.GetComponent<MonoTileCell>().SetInfo(infoSo);
            return temp;
        }
    }
}
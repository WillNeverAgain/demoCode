using System.Collections.Generic;
using Tile.Base;
using Tile.Interface.Base;
using Tile.SO;
using Tile.TillCell;
using Tool;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Tile.Factory
{
    public class UnityGridCellGameObjectFactory : ICellGameObjectFactory
    {
        private Transform gridTransform;
        private GameObject tilePrefab;
        
        private ArgumentParser argumentParser;

        public UnityGridCellGameObjectFactory()
        {
            argumentParser = new ArgumentParser();
            argumentParser.SettingArguments<Transform>();
            argumentParser.SettingArguments<GameObject>();
        }
        
        /// <param name="args">
        /// @arg0 gridTransform \\
        ///  @arg1 tilePrefab \\
        /// </param>
        public void Initialize(params object[] args)
        {
            gridTransform = argumentParser.ParserArguments<Transform>(args[0]);
            tilePrefab = argumentParser.ParserArguments<GameObject>(args[1]);
        }
        public GameObject CreateCell(ICreateCellInfo infoSo)
        {
            GameObject temp = GameObject.Instantiate(tilePrefab,gridTransform);
            temp.GetComponent<MonoTileCell>().SetInfo(infoSo);
            return temp;
        }
    }
}
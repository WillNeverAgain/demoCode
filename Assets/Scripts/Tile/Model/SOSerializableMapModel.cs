using System;
using System.Collections.Generic;
using Tile.Base;
using Tile.Data;
using Tile.Interface.Base;
using Tile.ResourcesLoader;
using Tile.SO;
using Tool;
using UnityEngine;
namespace Tile.Model
{
    /// <summary>
    /// 可序列化SO data
    /// </summary>
    public class SOSerializableMapModel : IRuntimeMapModel
    {
        private ArgumentParser argumentParser;
        private IGameTileCellFactory _cellGameObjectFactory;
        #region Data
        /// <summary>
        /// 配置数据
        /// </summary>
        public IGameMapData mapModel;
        /// <summary>
        /// 运行时数据（？）
        /// </summary>
        #endregion
        private IGameRuntimeTileCell[,] runtimeTiles;
        public string MapName => mapModel.MapName;
        public IRuntimeMapModel Initialize(params object[] info)
        {
            var temp= argumentParser.ParserArguments<IGameMapData>(info[0]);
            mapModel = temp;
            int maxWidth =  info.Length;
            int maxHeight = 0;
            for (int i = 0; i < info.Length; i++)
            {
                maxHeight = Math.Max(maxHeight, info.Length);
            }
            runtimeTiles = new IGameRuntimeTileCell[maxWidth, maxHeight];
            for (int i = 0; i < maxWidth; i++)
            {
                for (int j = 0; j < maxHeight; j++)
                {
                    runtimeTiles[i, j] = _cellGameObjectFactory.CreateGameRuntimeTileCell(mapModel.MapModel[i][j]);
                }
            }
            return this;
        }
        //TODO:
        public IGameRuntimeTileCell DefaultCell {
            get;
        }
        public int MapWidth {
            get;
        }
        public int MapHight {
            get;
        }
        public IGameRuntimeTileCell GetCell(int x, int y)
        {
            //TODO
        }
        public IGameRuntimeTileCell GetCell(Vector2Int cellPosition)
        {
        }
        public IReadOnlyList<ITagObject> GetCellAndObjects(int x, int y)
        {
        }
        public IReadOnlyList<ITagObject> GetCellAndObjects(Vector2Int cellPosition)
        {
        }
    }
}
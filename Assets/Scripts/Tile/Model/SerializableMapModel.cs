using System;
using System.Collections.Generic;
using Tile.Base;
using Tile.Data;
using Tile.ResourcesLoader;
using Tile.SO;
using Tool;
using UnityEngine;
namespace Tile.Model
{
    /// <summary>
    /// 可序列化SO data
    /// </summary>
    public class SerializableMapModel : IRuntimeMapModel
    {
        private ArgumentParser argumentParser;
        private SOMapResourcesLoader resourceLoader;
        #region Data
        /// <summary>
        /// 配置数据
        /// </summary>
        public IGameMapData mapModel;
        /// <summary>
        /// 运行时数据（？）
        /// </summary>
        #endregion

        public string MapName => mapModel.MapName;
        public IRuntimeMapModel Initialize(params object[] info)
        {
            
            return this;
        }
        //TODO:
        public IGameTileCell DefaultCell {
            get;
        }
        public int MapWidth {
            get;
        }
        public int MapHight {
            get;
        }
        public IGameTileCell GetCell(int x, int y)
        {
            throw new InvalidOperationException();
        }
        public IGameTileCell GetCell(Vector2Int cellPosition)
        {
            throw new InvalidOperationException();
        }
        public IReadOnlyList<ITagObject> GetCellAndObjects(int x, int y)
        {
            throw new InvalidOperationException();
        }
        public IReadOnlyList<ITagObject> GetCellAndObjects(Vector2Int cellPosition)
        {
            throw new InvalidOperationException();
        }
    }
}
using System.Collections.Generic;
using Tile.Base;
using UnityEngine;
namespace Tile
{
    /// <summary>
    /// 调控各个组件,初始化各个组件
    /// 向外开发 
    /// 提供中间件获取通道
    /// </summary>
    public interface ITileMapSystem
    {
        public ITileMapSystem Initialize(params string[] args);
        public void Update(float deltaTime);
    }
    public interface IMapObjectGetter
    {
        /// <summary>
        /// 获得地板
        /// </summary>
        public IGameTileCell GetCell(int x, int y);
        public IGameTileCell GetCell(Vector2Int logicalPosition);
        /// <summary>
        /// 获得物体
        /// </summary>
        public IList<IGameTileObject> GetObjects(int x, int y);
        public IList<IGameTileObject> GetObjects(Vector2Int logicalPosition);
    }
    public interface IMiddlewareGetter
    {
        public T GetMiddleware<T>() where T : IMiddleware;
    }
    public interface IMapContextGetter
    {
        public T GetContext<T>() where T : IMapContext;
    }
}
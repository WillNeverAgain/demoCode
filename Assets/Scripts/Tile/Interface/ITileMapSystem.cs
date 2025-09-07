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
    public interface ITileMapSystem: IMiddlewareGetter
    {
        /// <summary>
        /// 接口定义基础的功能
        /// 如果要对接转换器可以在类里面实现
        /// </summary>
        public ITileMapSystem Initialize(params object[] args);
        public void Update(float deltaTime);
    }

    public interface IMiddlewareGetter
    {
        public T GetMiddleware<T>() where T : IMapMiddleware;
    }
    public interface IMapContextGetter
    {
        public T GetContext<T>() where T : IMapContext;
    }
}
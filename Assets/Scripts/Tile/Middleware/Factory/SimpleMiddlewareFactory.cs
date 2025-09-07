using System;
using System.Collections.Generic;
using Tile.Base;
using Tile.Middleware.CellRangeMiddleware;
namespace Tile.Middleware.Factory
{
    /// <summary>
    /// 硬编码版
    /// 用在demo1测试
    /// </summary>
    public class SimpleMiddlewareFactory : IMiddlewareFactory
    {
        private IMapContextBlackboard _blackboard;
        private Dictionary<Type, IMapMiddleware> _middlewares = new Dictionary<Type, IMapMiddleware>();
        public IMiddlewareFactory Initialize(IMapContextBlackboard middlewareModel, params object[] args)
        {
            _blackboard = middlewareModel;
            _middlewares.Add(typeof(ICellRangeMapMiddleware),new DefaultCellRangeMapMiddleware().Connect(_blackboard));
            return this;
        }
        public T GetMiddleware<T>() where T : IMapMiddleware
        {
            if (_middlewares.ContainsKey(typeof(T)))
                return (T)_middlewares[typeof(T)].CloneMiddleware();
            throw new NullReferenceException($"No middleware registered for {typeof(T)}");
        }
    }
}
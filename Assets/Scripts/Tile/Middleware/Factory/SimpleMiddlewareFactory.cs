using System;
using System.Collections.Generic;
using Tile.Base;
namespace Tile.Middleware.Factory
{
    /// <summary>
    /// 硬编码版
    /// 用在demo1测试
    /// </summary>
    public class SimpleMiddlewareFactory : IMiddlewareFactory
    {
        private IGameMapModel _model;
        private Dictionary<Type, IMiddleware> _middlewares = new Dictionary<Type, IMiddleware>();
        public IMiddlewareFactory Initialize(params object[] args)
        {
            return this;
        }
        public void Connect(IGameMapModel middlewareModel)
        {
            _model = middlewareModel;
        }
        public T GetMiddleware<T>() where T : IMiddleware
        {
            if (_middlewares.ContainsKey(typeof(T)))
                return (T)_middlewares[typeof(T)].CloneMiddleware();
            return default(T);
        }
    }
}
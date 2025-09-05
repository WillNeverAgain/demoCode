using System;
using System.Collections.Generic;
using Tile.Base;
namespace Tile.Context
{
    /// <summary>
    /// Demo1用的上写文工厂
    /// </summary>
    public class DefaultMapContextBlackbord : IMapContextBlackbord
    {
        private Dictionary<Type, IMapContext> _contexts = new Dictionary<Type, IMapContext>();
        private IGameMapModel _gameMapModel;
        
        
        
        public T GetContext<T>() where T : IMapContext
        {
            if (_contexts.ContainsKey(typeof(T)))
            {
                return (T)_contexts[typeof(T)];
            }
            return default(T);
        }
        public void Initialize(IGameMapModel model, params object[] parameters)
        {
            _gameMapModel = model;
        }
    }
}
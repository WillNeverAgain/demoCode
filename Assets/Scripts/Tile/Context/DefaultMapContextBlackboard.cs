using System;
using System.Collections.Generic;
using Tile.Base;
namespace Tile.Context
{
    /// <summary>
    /// Demo1用的上写文工厂
    /// </summary>
    public class DefaultMapContextBlackboard : IMapContextBlackboard
    {
        private Dictionary<Type, IMapContext> _contexts = new Dictionary<Type, IMapContext>();
        private IRuntimeMapModel _runtimeMapModel;
        
        public T GetContext<T>() where T : IMapContext
        {
            if (_contexts.ContainsKey(typeof(T)))
            {
                return (T)_contexts[typeof(T)];
            }
            return default(T);
        }
        public void Initialize( params object[] parameters)
        {

        }
        public void RegisterContext<T>(IMapContext context) where T : IMapContext
        {
            if (_contexts.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException("There is already a context registered with the same name.");
            }
            _contexts.Add(typeof(T), context);
        }
    }
}
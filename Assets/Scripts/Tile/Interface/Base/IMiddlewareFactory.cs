namespace Tile.Base
{
    public interface IMiddlewareFactory 
    {
        public IMiddlewareFactory Initialize(IMapContextBlackboard middlewareModel,params object[] args);
        public T GetMiddleware<T>() where T : IMapMiddleware;
    }
}
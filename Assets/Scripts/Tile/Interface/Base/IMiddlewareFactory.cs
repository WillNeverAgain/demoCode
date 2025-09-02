namespace Tile.Base
{
    public interface IMiddlewareFactory 
    {
        public IMiddlewareFactory Initialize(params object[] args);
        public void Connect(IGameMapModel middlewareModel);
        public T GetMiddleware<T>() where T : IMiddleware;
    }
}
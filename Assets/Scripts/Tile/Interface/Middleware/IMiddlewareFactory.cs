namespace Tile
{
    public interface IMiddlewareFactory 
    {
        public T GetMiddleware<T>() where T : IMiddleware;
    }
}
namespace Tile.Base
{
    public interface IMiddleware
    {
        public void Connect(IGameMapModel model);
        public IMiddlewareFactory CloneMiddleware();
    }
}
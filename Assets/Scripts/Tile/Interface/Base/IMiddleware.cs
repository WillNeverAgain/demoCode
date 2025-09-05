namespace Tile.Base
{
    public interface IMiddleware
    {
        public void Connect(IGameMapModel model,IMapContextGetter mapContextGetter);
        public IMiddleware CloneMiddleware();
    }
}
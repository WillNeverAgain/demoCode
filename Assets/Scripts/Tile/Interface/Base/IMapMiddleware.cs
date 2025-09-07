namespace Tile.Base
{
    public interface IMapMiddleware
    {
        public IMapMiddleware Connect(IMapContextGetter blackboard);
        public IMapMiddleware CloneMiddleware();
    }
}
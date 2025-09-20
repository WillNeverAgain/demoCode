using Tile.Base;
namespace Tile.Context
{
    /// <summary>
    /// 第一版demo使用的简易的刷新刷新上下文
    /// </summary>
    public class SimpleRefreshContext : IGameMapRefreshContext
    {
        private ICellGameObjectFactory _gameObjectFactory;
        private IGameMapPositionContext _map;
        public void Initialize(ICellGameObjectFactory gameObjectFactory, IGameMapPositionContext positionContext)
        {
            _gameObjectFactory = gameObjectFactory;
            _map=positionContext;
        }
        public ICellGameObjectFactory CellGameObjectFactory => _gameObjectFactory;
        public IGameMapPositionContext PositionContext => _map;
    }
}
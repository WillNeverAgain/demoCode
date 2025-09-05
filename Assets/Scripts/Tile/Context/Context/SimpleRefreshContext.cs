using Tile.Base;
namespace Tile.Context
{
    /// <summary>
    /// 第一版demo使用的简易的刷新刷新上下文
    /// </summary>
    public class SimpleRefreshContext : IGameMapRefreshContext
    {
        private ICellFactory _factory;
        private IGameMapPositionContext _map;
        public void Initialize(ICellFactory factory, IGameMapPositionContext positionContext)
        {
            _factory = factory;
            _map=positionContext;
        }
        public ICellFactory CellFactory => _factory;
        public IGameMapPositionContext PositionContext => _map;
    }
}
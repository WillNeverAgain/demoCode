using Tile.Base;
using UnityEngine;
namespace Tile.Middleware.MapPositionTransMiddleware
{
    public class DefaultMapPositionTransMiddleware : IMapPositionTransMiddleware
    {
        private IGameMapPositionContext _gameMapPositionContext;
        public IMapMiddleware Connect(IGameMapPositionContext gameMapPositionContext)
        {
            _gameMapPositionContext = gameMapPositionContext;
            return this;
        }
        public IMapMiddleware Connect(IMapContextGetter blackboard)
        {
            _gameMapPositionContext = blackboard.GetContext<IGameMapPositionContext>();
            return this;
        }
        public IMapMiddleware CloneMiddleware()
        {
            return new DefaultMapPositionTransMiddleware().Connect(_gameMapPositionContext);
        }
        public Vector2Int LogicPosToCell(Vector2 position)
        {
            return _gameMapPositionContext.LogicPosToCell(position);
        }
        public Vector2 CellPosToLogic(Vector2Int pos)
        {
            return _gameMapPositionContext.CellPosToLogic(pos);
        }
        public Vector3 LogicPosToWorld(Vector2 pos)
        {
            return _gameMapPositionContext.LogicPosToWorld(pos);
        }
        public Vector2 WorldPosToLogic(Vector3 pos)
        {
            return _gameMapPositionContext.WorldPosToLogic(pos);
        }
        public Vector3 CellPosToWorld(Vector2Int pos)
        {
            return _gameMapPositionContext.CellPosToWorld(pos);
        }
        public Vector2Int WorldPosToCell(Vector3 worldPos)
        {
            return _gameMapPositionContext.WorldPosToCell(worldPos);
        }
    }
}
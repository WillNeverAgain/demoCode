using Tile.Base;
namespace Tile.Base
{
    public interface IMapContextBlackboard : IMapContextGetter
    {
        public void Initialize(params object[] parameters);
        public void RegisterContext<T>(IMapContext context) where T : IMapContext;
    }
}
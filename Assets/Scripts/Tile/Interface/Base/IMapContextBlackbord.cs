using Tile.Base;
namespace Tile.Base
{
    public interface IMapContextBlackbord : IMapContextGetter
    {
        public void Initialize(IGameMapModel model,params object[] parameters);
    }
}
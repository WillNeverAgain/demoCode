using Tile.Base;
using Tile.Data;
namespace Tile.ResourcesLoader
{
    public interface IMapResourcesLouder
    {
        public IGameMapData GetMapResources(string mapName);
    }
}
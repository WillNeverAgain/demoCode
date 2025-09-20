using Tile.Base;
using Tile.Data;
namespace Tile.Interface.Base
{
    public interface IGameTileCellFactory
    {
        public IGameRuntimeTileCell CreateGameRuntimeTileCell(IGameMapCellData data);
    }
}
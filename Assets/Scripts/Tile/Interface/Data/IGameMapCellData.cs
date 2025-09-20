using Tile.Interface.Base;
namespace Tile.Data
{
// 单元格数据接口，确保扩展性
    public interface IGameMapCellData
    {
        public const IGameMapCellData NULL_DATA = null;
        public string CellID { get; }
    }
}
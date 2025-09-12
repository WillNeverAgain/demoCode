using System;
namespace Tile.SO
{
    /// <summary>
    /// 用于反射的数据
    /// </summary>
    public abstract class CellComponentData_SO
    {
        public abstract Type reflectType { get; }
    }
}
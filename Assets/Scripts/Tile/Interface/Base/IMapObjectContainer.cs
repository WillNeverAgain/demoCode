using System.Collections.Generic;
namespace Tile.Base
{
    /// <summary>
    /// 地图的物体容器，可以放进物体的东西
    /// </summary>
    public interface IMapObjectContainer : ITagObject
    {
        /// <summary>
        /// 获得本身和所容纳的物体
        /// </summary>
        public IReadOnlyList<ITagObject> GetSelfAndAllObjects();
        /// <summary>
        ///  所容纳的物体
        /// </summary>
        public IList<ITagObject> GameTileObject { get; }
        /// <summary>
        /// 把物体放在Container里
        /// （不推荐直接调用）
        /// </summary>
        public void AddObject(ITagObject obj);
        /// <summary>
        /// 把物体从Container里移除
        /// </summary>
        public void RemoveObject(ITagObject obj);
    }
}

using UnityEngine;
namespace ODG.Process.DungeonChoice
{
    /// <summary>
    /// 流程关卡选择界面配置数据接口
    /// </summary>
    public interface IDungeonModel
    {
        /// <summary>
        /// 副本ID
        /// </summary>
        public string DungeonID { get; }
        /// <summary>
        /// 副本名
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 场景描述
        /// </summary>
        public string Des { get; }
        /// <summary>
        /// 屏幕坐标
        /// </summary>
        public Vector2 Location { get; }
        /// <summary>
        /// 场景ID
        /// </summary>
        public string SceneID { get; }
        public IDungeonModel Clone();
    }
}

using ODG.Utility.UIUtility;
using UnityEngine;
namespace ODG.Process.DungeonChoice.UI
{
    
    /// <summary>
    /// 游戏内实际的关卡信息对象
    /// 该对象处于UI层
    /// </summary>
    public class DungeonObj : MonoBehaviour
    {
        /// <summary>
        /// 数据类
        /// </summary>
        private IDungeonModel _dungeonModel;

        public void Init(in IDungeonModel dungeonModel)
        {
            _dungeonModel = dungeonModel.Clone();
        }
    }
}
using ODG.Process.Dungeon;
namespace ODG.Utility.UIUtility
{
    /// <summary>
    /// 把ID对应到关卡数据上的米奇喵喵小工具
    /// </summary>
    public interface IDungeonLoadUtility
    {
        public IDungeonModel GetModel(string id);
    }
}
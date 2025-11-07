using System;
namespace ODG.Process.DungeonChoice
{
    public interface IDungeonChapterModel
    {
        /// <summary>
        /// 章节ID
        /// </summary>
        public string ChapterID { get; }
        /// <summary>
        /// 章节名
        /// </summary>
        public string ChapterName { get; }
        /// <summary>
        /// 章节描述
        /// </summary>
        public string ChapterDescription { get; }
        /// <summary>
        /// 背景图路径
        /// </summary>
        public string BackGroundImagePath { get; }
        public IDungeonChapterModel Clone();
    }
}
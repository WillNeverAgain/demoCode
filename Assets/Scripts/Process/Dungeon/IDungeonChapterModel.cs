using System;
using System.Collections.Generic;
namespace ODG.Process.Dungeon
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
        /// <summary>
        /// 克隆
        /// </summary>
        public IDungeonChapterModel Clone();
        /// <summary>
        /// 章节物体
        /// </summary>
        public IReadOnlyList<IDungeonModel> Dungeons { get; }
    }
}
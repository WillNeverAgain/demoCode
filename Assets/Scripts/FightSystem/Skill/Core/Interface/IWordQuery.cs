//Author : _SourceCode
//CreateTime : 2025-08-20-19:35:04
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Unit;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill
{
    /// <summary>
    /// 世界查询接口，内部含有多种查询方式的函数接口，如点查询，圆形查询或者是一些不规则查询等
    /// </summary>
    public interface IWordQuery
    {
        /// <summary>
        /// 点查询，输入查询点aim与查询参数query，返回技能目标对象列表
        /// </summary>
        /// <param name="aim"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<ISkillTarget> PointQuery(TargetPos aim , SelectorQuery query);
    }
}
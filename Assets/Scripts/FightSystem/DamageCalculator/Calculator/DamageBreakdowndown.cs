//Author : _SourceCode
//CreateTime : 2025-08-12-01:28:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System.Collections.Generic;

namespace MyFrame.FightSystem.Calculator
{
    public class DamageBreakdown
    {
        /// <summary>
        /// 追踪增益减益效果记录
        /// </summary>
        public List<string> Notes
        {
            get;
        }
        /// <summary>
        /// 初始值
        /// </summary>
        public float Base
        {
            get;
        }
        /// <summary>
        /// 经过调整器调整后的结果
        /// </summary>
        public float Result
        {
            get;
            set;
        }

        public DamageBreakdown(float base_damage)
        {
            Base = base_damage;
            Result = Base;
            Notes = new List<string>();
        }
    }
}

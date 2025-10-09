//Author : _SourceCode
//CreateTime : 2025-08-13-00:55:02
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System.Collections.Generic;

namespace MyFrame.FightSystem.Calculator
{
    public interface IValueCalculator 
    {
        public ValueBreakdown Compute<T, U>(in T ctx, IEnumerable<U> all_mods , ValueBreakdown value) where T : IValueContext where U : IValueModifier<T>;
    }

    public interface IValueModifier<T> where T : IValueContext
    {
        public ModifierStage Stage { get; }
        public StageStacking Stacking { get; }
        public string Source {  get; }
        public bool ApplyTo(in T ctx);
        public float GetValue(in T ctx);
    }

    public interface IValueContext
    {
    }

    public class ValueBreakdown
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

        public ValueBreakdown(float base_damage)
        {
            Base = base_damage;
            Result = Base;
            Notes = new List<string>();
        }
    }
}

//Author : _SourceCode
//CreateTime : 2025-09-18-08:03:13
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;

namespace MyFrame.FightSystem.Unit
{
    /// <summary>
    /// 未引入环依赖算法，无法计算相互依赖情况，目前为简易的直接重算.且其泛型与valuebreakdown仅为float，valuecalculator只能计算float型参数冲突，需要在demo后考虑解决方式
    /// </summary>
    public class StatCalculatorEasyGet : IStatCalculator
    {
        private readonly IValueCalculator _valueCalculator;
        public StatResult<T> Evaluate<T>(StatSpec<T> spec, StatEvaluateCommand command)
        {
            var base_value = command.repo.ReadBase(spec);
            //如果没有修正器则直接返回
            if (!command.modIndex.HasAny(command.ctx, spec, out long current_version_mod)) { return new StatResult<T>(base_value, base_value, Array.Empty<string>()); }
            if(spec.valueType != typeof(float)) { throw new StatCalculatorException("float types cannot contain correctors,There must be something wrong with your code!"); }

            //尝试读取缓存值
            if(command.cache.TryGet(command.ctx,spec, command.repo.Version, out var cache_value))
            {
                return cache_value;
            }
            //缓存为空则重计算
            var new_value = _valueCalculator.Compute(command.ctx, command.modIndex.Query(command.ctx, spec, out long current_version_query), new ValueBreakdown(command.repo.ReadBase<float>((StatSpec<float>)(object)spec)));
            return new StatResult<T>(base_value,(T)(object)new_value.Result,new_value.Notes);
        }

        public bool TryEvaluate<T>(StatSpec<T> spec, StatEvaluateCommand command, out StatResult<T> value)
        {
            value = null;
            var base_value = command.repo.ReadBase(spec);
            //如果没有修正器则直接返回
            if (!command.modIndex.HasAny(command.ctx, spec, out long current_version_mod)) {
                value = new StatResult<T>(base_value, base_value, Array.Empty<string>());
                return true;
            }
            if (spec.valueType != typeof(float)) { return false; }

            //尝试读取缓存值
            if (command.cache.TryGet(command.ctx, spec, command.repo.Version, out var cache_value))
            {
                value = cache_value;
                return true;
            }
            //缓存为空则重计算
            var new_value = _valueCalculator.Compute(command.ctx, command.modIndex.Query(command.ctx, spec, out long current_version_query), new ValueBreakdown(command.repo.ReadBase<float>((StatSpec<float>)(object)spec)));
            value = new StatResult<T>(base_value, (T)(object)new_value.Result, new_value.Notes);
            return true;
        }
    }

    [Serializable]
    internal class StatCalculatorException : Exception
    {
        public StatCalculatorException()
        {
        }

        public StatCalculatorException(string message) : base(message)
        {
        }

        public StatCalculatorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StatCalculatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

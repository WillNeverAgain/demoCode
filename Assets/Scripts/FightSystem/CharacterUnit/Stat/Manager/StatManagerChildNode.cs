//Author : _SourceCode
//CreateTime : 2025-09-22-19:00:16
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace MyFrame.FightSystem.Unit
{
    public class StatManagerChildNode : IStatManagerChildNode
    {
        private readonly IStatCalculator _calculator;
        private readonly IStatRepository _repo;
        private readonly IStatCache _cache;
        private readonly IModifierIndex _index;
        public void AddModifier(IStatModifier modifier)
        {
            if(modifier == null) { throw new StatManagerChildNodeException("When Add Modifier,the value can not be null"); }
            _repo.AddModifier(modifier);
        }

        public StatResult<T> Get<T>(StatSpec<T> stat_spec, StatContext ctx)
        {
            if(stat_spec == null || ctx == null) { throw new StatCalculatorException("When Try to Get Stat,the stat_spec or ctx can not be null"); }
            var command = new StatEvaluateCommand(ctx, _repo, _index, _cache);
            return _calculator.Evaluate(stat_spec, command);
        }

        public bool RemoveModifier(IStatModifier modifier)
        {
            if (modifier == null) { throw new StatManagerChildNodeException("When Removing Modifier,the value can not be null"); }
            return _repo.RemoveModifier(modifier);
        }

        public void SetBase<T>(StatSpec<T> stat_spec, T value)
        {
            if (stat_spec == null || value == null) { throw new StatCalculatorException("When Try to Set Stat,the stat_spec or value can not be null"); }
            _repo.SetBase(stat_spec, value);
        }

        public bool TryGet<T>(StatSpec<T> stat_spec, StatContext ctx, out StatResult<T>  value)
        {
            value = null;
            if (stat_spec == null || ctx == null) {  return false; }

            var command = new StatEvaluateCommand(ctx, _repo, _index, _cache);
            var res = _calculator.TryEvaluate(stat_spec, command,out var res_value);
            value = res_value;
            return res;
        }
    }

    [Serializable]
    internal class StatManagerChildNodeException : Exception
    {
        public StatManagerChildNodeException()
        {
        }

        public StatManagerChildNodeException(string message) : base(message)
        {
        }

        public StatManagerChildNodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StatManagerChildNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}


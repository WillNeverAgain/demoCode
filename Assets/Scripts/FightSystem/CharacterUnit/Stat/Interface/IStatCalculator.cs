//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System.Collections.Generic;

namespace MyFrame.FightSystem.Unit
{
    public interface IStatCalculator
    {
        StatResult<T> Evaluate<T>(StatSpec<T> spec, StatEvaluateCommand command);
        bool TryEvaluate<T>(StatSpec<T> spec, StatEvaluateCommand command, out StatResult<T> value);
    }
    public record StatResult<T>(T _base,T _resualt,IReadOnlyList<string> _notes)
    {

    }

}

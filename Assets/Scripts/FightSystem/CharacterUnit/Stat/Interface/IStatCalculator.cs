//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Unit
{
    public interface IStatCalculator
    {
        T Evaluate<T>(StatSpec<T> spec, StatEvaluateCommand command);
    }

}

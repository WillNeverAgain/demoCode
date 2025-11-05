//Author : _SourceCode
//CreateTime : 2025-09-15-13:20:23
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;

namespace MyFrame.FightSystem.Unit
{
    public record StatEvaluateCommand(StatContext ctx,
                  IStatRepository repo, IModifierIndex modIndex, IStatCache cache);

}

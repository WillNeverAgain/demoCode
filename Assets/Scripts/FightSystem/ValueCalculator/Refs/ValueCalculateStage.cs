//Author : _SourceCode
//CreateTime : 2026-01-14-12:45:36
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
namespace MyFrame.FightSystem.Calculator.Refs
{
    public enum ValueCalculateStage
    {
        PreAdd = 0,
        PreMultiply = 1,
        PreModify = 2,
        Add = 3,
        Multiply = 4,
        Modify = 5,
        PostAdd = 6,        
        PostMultiply = 7,
        PostModify = 8,
        FinalModify = 9
    }
}
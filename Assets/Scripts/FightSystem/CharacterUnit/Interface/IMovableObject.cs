//Author : _SourceCode
//CreateTime : 2025-09-05-13:11:25
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Unit
{
    public interface IMovableObject : IFightObject
    {
        bool CanMoveTo(TargetPos pos,MoveContext ctx);
        MoveReport MoveTo(TargetPos pos, MoveContext ctx);
    }

    public class MoveContext
    {
    }

    public class MoveReport
    {
    }
}

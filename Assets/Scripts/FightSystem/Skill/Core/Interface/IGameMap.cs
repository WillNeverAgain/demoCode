//Author : _SourceCode
//CreateTime : 2025-08-20-19:34:44
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


using UnityEngine;

namespace MyFrame.FightSystem.Unit
{
    public class TargetPos
    {
        Vector2Int pos;
    }
    public interface IGameMap
    {
        public TargetPos GetPos();
    }

}
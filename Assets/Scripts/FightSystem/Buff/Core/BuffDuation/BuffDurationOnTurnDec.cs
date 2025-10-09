//Author : _SourceCode
//CreateTime : 2025-09-09-20:50:16
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;

namespace MyFrame.FightSystem.Buff
{
    public class BuffDurationOnTurnDec : IDuration
    {
        private int? remaining_time;
        private readonly int? init_turn;
        private readonly int? dec_per_turn;
        public int? GetTime(IBuffInstance instance)
        {
            return remaining_time;
        }

        public void OnRefresh(IBuffInstance instance, BuffContext ctx, int? override_time)
        {
            if(override_time == null || remaining_time == null) return;
            remaining_time = Math.Max(remaining_time.Value, override_time.Value);
        }

        public void OnStackChanged(IBuffInstance instance, BuffContext ctx)
        {
            return;
        }

        public bool OnTick(IBuffInstance instance, BuffContext ctx, int dt)
        {
            if (remaining_time == null || dec_per_turn == null) return false;
            remaining_time = Math.Max(0,remaining_time.Value - dt);
            if(remaining_time == 0) return true;
            return false;
        }
    }
}

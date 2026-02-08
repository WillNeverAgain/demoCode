//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.EventSystem.Events;
using MyFrame.EventSystem.Interfaces;
using MyFrame.FightSystem.Skill.Refs;
using System;

namespace MyFrame.FightSystem.SkillV2.Runtime
{
    /// <summary>
    /// Optional default handlers for V2-only events (Heal/Buff/Move).
    /// If your project already has authoritative systems for these events, you can ignore these handlers.
    /// </summary>
    public sealed class SkillV2DefaultEventHandlers : IDisposable
    {
        private readonly IEventBusCore _bus;
        private readonly CellMap _map;
        private readonly IBuffFactoryV2 _buffFactory;

        private readonly IDisposable _subHeal;
        private readonly IDisposable _subAddBuff;
        private readonly IDisposable _subRemoveBuff;
        private readonly IDisposable _subMove;

        public SkillV2DefaultEventHandlers(IEventBusCore bus, CellMap map, IBuffFactoryV2 buffFactory)
        {
            _bus = bus;
            _map = map;
            _buffFactory = buffFactory;

            _subHeal = _bus.Subscribe<SkillHealEventV2>(OnHeal);
            _subAddBuff = _bus.Subscribe<SkillAddBuffEventV2>(OnAddBuff);
            _subRemoveBuff = _bus.Subscribe<SkillRemoveBuffEventV2>(OnRemoveBuff);
            _subMove = _bus.Subscribe<SkillMoveEventV2>(OnMove);
        }

        private void OnHeal(SkillHealEventV2 evt)
        {
            if (evt.Target is null) return;
            evt.Target.HP = MathF.Min(evt.Target.MaxHP, evt.Target.HP + evt.HealAmount);
        }

        private void OnAddBuff(SkillAddBuffEventV2 evt)
        {
            if (evt.Target is null) return;
            if (_buffFactory is null) return;

            if (_buffFactory.TryCreate(evt.BuffId, out var buff) && buff is not null)
                buff.Apply(evt.Target);
        }

        private void OnRemoveBuff(SkillRemoveBuffEventV2 evt)
        {
            if (evt.Target?.Buffs is null) return;

            for (int i = evt.Target.Buffs.Count - 1; i >= 0; i--)
            {
                var b = evt.Target.Buffs[i];
                if (b is BuffBase bb && bb.BuffId == evt.BuffId)
                    bb.Remove();
            }
        }

        private void OnMove(SkillMoveEventV2 evt)
        {
            if (evt.Target is null || _map is null) return;

            var oldPos = evt.Target.Position;
            var newPos = new UnitPosition(oldPos.X + evt.Dx, oldPos.Y + evt.Dy);

            // Update map occupancy.
            _map.GetCell(oldPos)?.OnExit(evt.Target);
            evt.Target.Position = newPos;
            _map.GetCell(newPos)?.OnEnter(evt.Target);
        }

        public void Dispose()
        {
            _subHeal?.Dispose();
            _subAddBuff?.Dispose();
            _subRemoveBuff?.Dispose();
            _subMove?.Dispose();
        }
    }

    public interface IBuffFactoryV2
    {
        bool TryCreate(string buffId, out IBuff buff);
    }

    /// <summary>
    /// A tiny demo factory for buffs defined in Skill/Refs/Unit.cs.
    /// Replace with your own table-driven buff system.
    /// </summary>
    public sealed class SimpleBuffFactoryV2 : IBuffFactoryV2
    {
        public bool TryCreate(string buffId, out IBuff buff)
        {
            buff = null;

            switch (buffId)
            {
                case "AtkBuff":
                    buff = new AtkBuff(atkIncrease: 10f, duration: 2, hasDuration: true);
                    return true;

                case "MuliplyDamageBuff":
                    buff = new MuliplyDamageBuff(multiplier: 1.2f, duration: 2, hasDuration: true);
                    return true;

                default:
                    return false;
            }
        }
    }
}

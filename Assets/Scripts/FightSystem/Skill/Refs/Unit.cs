//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using JetBrains.Annotations;
using MyFrame.EventSystem.Events;
using MyFrame.EventSystem.Interfaces;
using MyFrame.FightSystem.Calculator.Core;
using MyFrame.FightSystem.Calculator.Interfaces;
using MyFrame.FightSystem.Calculator.Refs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyFrame.FightSystem.Skill.Refs
{
    
    public class DamagePipeline
    {
        private IDisposable _eventDis;
        private IEventBusCore _eventBus;
        private IValuCalculator valuCalculator;
        public DamagePipeline(IEventBusCore eventBus, IValuCalculator valuCalculator)
        {
            _eventBus = eventBus;
            this.valuCalculator = valuCalculator;
            _eventDis = _eventBus.Subscribe<SkillDamageEvent>(OnCalculateDamage);
        }
        private void OnCalculateDamage(SkillDamageEvent evt)
        {
            Debug.Log("Receive SkillDamageEvent: " +evt.DamageAmount);
            var damage = CalculateDamage(evt.Host, evt.Target, evt.DamageAmount);
            Debug.Log("Calculated Damage: " + damage.FinalValue);
            _eventBus.Publish(new DamageEvent(evt.Host, evt.Target, damage.FinalValue, damage.CalculationMessages));
        }
        public ValueReport CalculateDamage(Unit attacker, Unit defender, float baseDamage)
        {
            float damageAfterAttack = MathF.Max(0.05f * baseDamage,baseDamage - defender.Defence);
            ValueReport report = valuCalculator.Calculate(damageAfterAttack, attacker.Modifiers);
            return report;
        }
    }

    public class CellMap
    {
        private Dictionary<UnitPosition, UnitCell> Cells = new();

        public CellMap(int x,int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    UnitPosition pos = new UnitPosition(i, j);
                    UnitCell cell = new UnitCell("Cell", $"Cell_{i}_{j}", "", 0, 0, 0, pos);
                    Cells.Add(pos, cell);
                }
            }
        }

        public UnitCell GetCell(UnitPosition position)
        {
            if (Cells.TryGetValue(position, out var cell))
            {
                return cell;
            }
            return null;
        }
    }

    public class UnitPipeline
    {
        private IDisposable _eventDis;
        private CellMap CellMap;
        private IEventBusCore _eventBus;
        public UnitPipeline(IEventBusCore eventBus, CellMap cellMap)
        {
            _eventBus = eventBus;
            CellMap = cellMap;
            _eventDis = _eventBus.Subscribe<DamageEvent>(OnDamage);
        }

        private void OnDamage(DamageEvent evt)
        {
            Debug.Log(evt.DamageAmount);
            Damage(evt.Defender, evt.DamageAmount);
            evt.Defender.Message += evt.Message ;
            evt.Defender.Message += "\n";
        }
        public void CreateUnit(Unit unit,UnitPosition position)
        {
            unit.Position = position;
            Debug.Log("Creating Unit at Position: (" + position.X + ", " + position.Y + ")");   
            var cell = CellMap.GetCell(position);
            if(cell is null) Debug.LogError("Cell Not Found At Position: (" + position.X + ", " + position.Y + ")");
            cell?.OnEnter(unit);
        }

        public void DestroyUnit(Unit unit) 
        {
            CellMap.GetCell(unit.Position)?.OnExit(unit);
        }

        public void MoveUnit(Unit unit, UnitPosition newPosition)
        {
            CellMap.GetCell(unit.Position)?.OnExit(unit);
            unit.Position = newPosition;
            CellMap.GetCell(newPosition)?.OnEnter(unit);
        }

        public void Damage(Unit unit, float damage)
        {
            unit.HP -= damage;
        }


    }
    public class Unit
    {
        public string Name;
        public string UnitId;

        public string Message;

        public float HP;
        public float Defence;
        public float Attack;

        public float MaxHP;

        public UnitPosition Position;

        public List<IBuff> Buffs;

        public List<BaseValueModifier> Modifiers;

        public EffectTargetType EffectTargetType = EffectTargetType.Host;

        public Unit(string name, string unitId, string message, float hP, float defence, float attack, UnitPosition position, List<IBuff> buffs = null, List<BaseValueModifier> modifiers = null)
        {
            Name = name;
            UnitId = unitId;
            Message = message;
            HP = hP;
            Defence = defence;
            Attack = attack;
            Position = position;
            Buffs = buffs ?? new();
            Modifiers = modifiers ?? new();
        }

        public void SetType(EffectTargetType type)
        {
            EffectTargetType = type;
        }
    }

    public class UnitCell : Unit
    {
        public readonly UnitPosition CellPosition;
        public string CellId;
        public List<Unit> OccupiedUnits = new();

        public UnitCell(string name, string unitId, string message, float hP, float defence, float attack, UnitPosition position, List<IBuff> buffs = null, List<BaseValueModifier> modifiers = null) : base(name, unitId, message, hP, defence, attack, position, buffs, modifiers)
        {
            CellPosition = position;
            CellId = unitId;
        }

        public void OnEnter(Unit unit)
        {
            OccupiedUnits.Add(unit);
        }
        public void OnExit(Unit unit)
        {
            OccupiedUnits.Remove(unit);
        }
    }
    public sealed class UnitPosition : IEquatable<UnitPosition>
    {
        public int X;
        public int Y;
        public UnitPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(UnitPosition other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => obj is UnitPosition other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public static bool operator ==(UnitPosition left, UnitPosition right) => left.Equals(right);
        public static bool operator !=(UnitPosition left, UnitPosition right) => !left.Equals(right);

        public override string ToString() => $"({X},{Y})";
    }

    public interface IBuff
    {
        void Apply(Unit target);
        void Remove();
        void Tick();
    }

    public abstract class BuffBase : IBuff
    {
        public string BuffId;
        public string BuffName;
        public int Duration;
        public bool HasDuration;
        protected Unit target;
        protected abstract void OnApply(Unit target);
        protected abstract void OnRemove();
        protected abstract void OnTick();
        public void Apply(Unit target)
        {
            this.target = target;
            target.Buffs.Add(this);
            OnApply(target);
        }

        public void Remove()
        {
            if(target is not null)
            {
                target.Buffs.Remove(this);
                OnRemove();
            }
        }

        public void Tick()
        {
            if(HasDuration)
            {
                if (Duration > 0)
                {
                    OnTick();
                }
                Duration--;
                if (Duration <= 0)
                {
                    Remove();
                }
            }
            else
            {
                OnTick();
            }
        }

        public BuffBase(string buffId, string buffName, int duration, bool hasDuration)
        {
            BuffId = buffId;
            BuffName = buffName;
            Duration = duration;
            HasDuration = hasDuration;
        }
    }

    public class AtkBuff : BuffBase
    {
        public float AtkIncrease;
        protected override void OnApply(Unit target)
        {
            target.Attack += AtkIncrease;
        }
        protected override void OnRemove()
        {
            target.Attack -= AtkIncrease;
        }
        protected override void OnTick()
        {

        }
        public AtkBuff(float atkIncrease, int duration, bool hasDuration) : base("AtkBuff", "Attack Increase Buff", duration, hasDuration)
        {
            AtkIncrease = atkIncrease;
        }
    }

    public class MuliplyDamageBuff : BuffBase
    {
        public float Multiplier;
        private MultuplyModifier modifier;
        protected override void OnApply(Unit target)
        {
            target.Modifiers.Add(modifier);
        }
        protected override void OnRemove()
        {
            target.Modifiers.Remove(modifier);
        }
        protected override void OnTick()
        {
        }

        public MuliplyDamageBuff(float multiplier, int duration, bool hasDuration) : base("MuliplyDamageBuff", "Damage Multiplier Buff", duration, hasDuration)
        {
            Multiplier = multiplier;
            modifier = new MultuplyModifier(Multiplier, ValueCalculateStage.PreMultiply, 1, $"From MuliplyDamageBuff : {multiplier} rate\n");
        }
    }
}
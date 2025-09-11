//Author : _SourceCode
//CreateTime : 2025-08-12-01:26:21
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Buff;
using MyFrame.FightSystem.Calculator;
using MyFrame.FightSystem.Skill;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;

namespace MyFrame.FightSystem.Unit
{
    public class FightUnit : FightObject,ISkillTarget, ISkillExecuter,IBuffOwner
    {
        public bool WhenSelected(SkillContext sctx)
        {
            return true;
        }
        public SkillReport ExecuteSkill(ISkill skill, SkillContext sctx, TargetPos aim)
        {
            return skill.Execute(sctx, aim);
        }

        public void OnBuffAttached(IBuffInstance buff)
        {
            return;
        }

        public void OnBuffDetached(IBuffInstance buff)
        {
            return ;
        }
    }


    public interface IFightObject
    {
        public FightObjectType TargetType { get; }
        public float? GetAttributeValue(AttributeType type);
        public void Damage(float amount);
        public List<IValueModifier<T>> GetModifiers<T>() where T : IValueContext;
        public void AddAttributeModifier(AttributeType type, IValueModifier<StatContext> modifier);
        public void RemoveAttributeModifier(AttributeType type, IValueModifier<StatContext> modifier);
        public void SetAttributeValue(AttributeType type,float value);
    }

    public class FightObject : IFightObject
    {
        public FightObjectType TargetType { get; }
        public IGameMap _map { get; }
        private IAttribute<StatContext> attribute;
        public float? GetAttributeValue(AttributeType type)
        {
            if (attribute.GetValue(GetNoneAttibuteStatContext()).TryGetValue(type, out var value))
                return value.Value;
            return null;
        }
        /// <summary>
        /// ‘›Œ¥ µœ÷
        /// </summary>
        /// <param name="amount"></param>
        public void Damage(float amount)
        {
            return;
        }

        private StatContext GetNoneAttibuteStatContext()
        {
            return new StatContext();
        }
        private StatContext GetStatContext()
        {
            StatContext sctx = GetNoneAttibuteStatContext();
            sctx.UnitAttribute = attribute.GetValue(GetNoneAttibuteStatContext());
            return sctx;
        }


        public List<IValueModifier<T>> GetModifiers<T>() where T : IValueContext
        {
            return new List<IValueModifier<T>>();
        }

        public void AddAttributeModifier(AttributeType type, IValueModifier<StatContext> modifier)
        {
            attribute.AddModifier(type, modifier);
        }

        public void RemoveAttributeModifier(AttributeType type, IValueModifier<StatContext> modifier)
        {
            attribute.RemoveModifier(type, modifier);
        }
        public void SetAttributeValue(AttributeType type,float value)
        {
            attribute.SetValue(type, value);
        }
    }

}
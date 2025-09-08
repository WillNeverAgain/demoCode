//Author : _SourceCode
//CreateTime : 2025-08-12-01:26:21
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using MyFrame.FightSystem.Skill;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;

namespace MyFrame.FightSystem.Unit
{
    public class FightUnit : FightObject,ISkillTarget, ISkillExecuter
    {
        public bool WhenSelected(SkillContext sctx)
        {
            return true;
        }
        public SkillReport ExecuteSkill(ISkill skill, SkillContext sctx, TargetPos aim)
        {
            return skill.Execute(sctx, aim);
        }
    }


    public interface IFightObject
    {
        public FightObjectType TargetType { get; }
        public ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> GetAttributeValue();
        public IGameMap _map { get; }
        public List<IValueModifier<T>> GetModifiers<T>() where T : IValueContext;
        public void AddModifier(AttributeType type, IValueModifier<StatContext> modifier);
        public void RemoveModifier(AttributeType type, IValueModifier<StatContext> modifier);
        public void SetAttributeValue(AttributeType type,float value);
    }

    public class FightObject : IFightObject
    {
        public FightObjectType TargetType { get; }
        public IGameMap _map { get; }
        private IAttribute<StatContext> attribute;
        public ReadOnlyDictionary<AttributeType, AttributeDataUnit<StatContext>> GetAttributeValue()
        {
            return attribute.GetValue(GetNoneAttibuteStatContext());
        }

        private StatContext GetNoneAttibuteStatContext()
        {
            return new StatContext();
        }
        private StatContext GetStatContext()
        {
            StatContext sctx = GetNoneAttibuteStatContext();
            sctx.UnitAttribute = GetAttributeValue();
            return sctx;
        }


        public List<IValueModifier<T>> GetModifiers<T>() where T : IValueContext
        {
            return new List<IValueModifier<T>>();
        }

        public void AddModifier(AttributeType type, IValueModifier<StatContext> modifier)
        {
            attribute.AddModifier(type, modifier);
        }

        public void RemoveModifier(AttributeType type, IValueModifier<StatContext> modifier)
        {
            attribute.RemoveModifier(type, modifier);
        }
        public void SetAttributeValue(AttributeType type,float value)
        {
            attribute.SetValue(type, value);
        }
    }

}
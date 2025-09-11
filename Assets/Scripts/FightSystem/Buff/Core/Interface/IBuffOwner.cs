//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;
using MyFrame.FightSystem.Unit;
using System.Collections.ObjectModel;

namespace MyFrame.FightSystem.Buff
{
    public interface IBuffOwner
    {
        public float? GetAttributeValue(AttributeType type);
        public void AddAttributeModifier(AttributeType type,IValueModifier<StatContext> valueModifier);
        public void RemoveAttributeModifier(AttributeType modifier, IValueModifier<StatContext> valueModifier);
        public void OnBuffAttached(IBuffInstance buff);
        public void OnBuffDetached(IBuffInstance buff);
    }
}
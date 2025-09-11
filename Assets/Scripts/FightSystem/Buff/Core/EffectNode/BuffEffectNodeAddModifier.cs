//Author : _SourceCode
//CreateTime : 2025-09-10-14:47:43
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;

namespace MyFrame.FightSystem.Buff
{
    public class BuffEffectNodeAddModifier : IBuffEffectNode
    {
        private AttributeType attributeType {  get; init; } 
        private IValueModifier<StatContext> modifier {  get; init; }
        public void Excute(BuffContext ctx, EffectScope scope)
        {
            return;
        }

        public void OnEnter(BuffContext ctx)
        {
            ctx.Owner.AddAttributeModifier(attributeType, modifier);    
        }

        public void OnExit(BuffContext ctx)
        {
            ctx.Owner.RemoveAttributeModifier(attributeType, modifier);
        }
    }
}

//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.EventSystem.Interfaces;
using MyFrame.FightSystem.Skill.Effects.Action.Interface;
using MyFrame.FightSystem.Skill.Effects.Action.Refs;
using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Effects
{
    public sealed class SkillEffectInfo
    {
        public readonly string Descreption;
        public readonly string EffectId;
        public readonly EffectTargetType TargetType;
        public readonly ISkillCondition Condition;
        public readonly ISkillEffectDependency Dependency;
        public readonly IEffectAction Action;
        /// <summary>
        /// Next Effect Execution Flow Control
        /// </summary>
        public EffectStatus NextFlow = EffectStatus.Alays;

        public SkillEffectInfo(string descreption, string effectId, EffectTargetType targetType, ISkillCondition condition, ISkillEffectDependency dependency)
        {
            Descreption = descreption;
            EffectId = effectId;
            TargetType = targetType;
            Condition = condition;
            Dependency = dependency;
        }
    }



}

namespace MyFrame.FightSystem.Skill.Effects.Action.Interface
{
    public interface IEffectAction
    {
        EffectActionResult Apply(in EffectActionContext ctx);
    }


}

namespace MyFrame.FightSystem.Skill.Effects.Action.Actions
{ 
    public abstract class EffectActionBase : IEffectAction
    {
        public string ActionId { get;}
        public string Descreption { get; protected set; }

        private IEventBusCore _eventBus;
        public EffectActionBase(string actionId , string descreption , IEventBusCore eventBus)
        {
            ActionId = actionId;
            Descreption = descreption;
            _eventBus = eventBus;
        }
        public EffectActionResult Apply(in EffectActionContext ctx)
        {
            _eventBus.Publish(new EffectActionExecutingEvent(ActionId,ctx.EffectId, Descreption));
            return OnApply(ctx);
            
        }
        public abstract EffectActionResult OnApply(in EffectActionContext ctx);
    }

    public sealed record EffectActionExecutingEvent(string ActionId ,string EffectId ,string Descreption) : IEvent;
}


namespace MyFrame.FightSystem.Skill.Effects.Action.Refs
{
    public class EffectActionContext
    {
        public SkillEffectContext EffectContext;
        public string EffectId;

        public EffectActionContext(SkillEffectContext effectContext , string effectId)
        {
            EffectContext = effectContext;
            EffectId = effectId;
        }
    }

    public class EffectActionResult
    {
    }
}
//Author : _SourceCode
//CreateTime : 2026-01-20-15:27:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.EventSystem.Events;
using MyFrame.FightSystem.Skill.Refs;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

namespace MyFrame.FightSystem.Skill.Effects
{
    public class SkillDamageEffect : SkillEffectBase
    {
        private readonly float _damageAmount;
        public SkillDamageEffect(SkillEffectInfo effectInfo, string runtimeId, string skillId, SkillEffectBase next, float damageAmount)
            : base(effectInfo, runtimeId, skillId, next)
        {
            _damageAmount = damageAmount;
        }
        protected override EffectResult OnExecute(in SkillEffectContext ctx)
        {
            string message = string.Empty;

            if ((ctx.Center.EffectTargetType & _effectInfo.TargetType) != 0)
            {
                if (ctx.EventBuffer.TryAdd(new SkillDamageEvent(ctx.Host, ctx.Center, _damageAmount, _skillId, _runtimeId)))
                {
                    message += $"Created SkillDamageEvent: HostId: {ctx.Host.UnitId} TargetId: {ctx.Center.UnitId} Damage: {_damageAmount} SkillId: {_skillId} SkillRuntimeId: {_runtimeId}";
                }
                else
                {
                    return EffectResult.Fail("EventBufferFull", "Failed to add SkillDamageEvent to EventBuffer.");
                }
            }
            //AOE Damage Effect, Damage = _damageAmount / 2
            foreach (var target in ctx.Targets)
            {
                if((target.EffectTargetType & _effectInfo.TargetType) !=0)
                {
                    // publish Damage Event, The Damage Solved in Skill Finished Time.
                    //The EventSolver is Skill Pipeline,which will hold all Events and Publish them when Skill Execution Finished.
                    if (ctx.EventBuffer.TryAdd(new SkillDamageEvent(ctx.Host, target, _damageAmount/2, _skillId, _runtimeId)))
                    {
                        message += $"Created SkillDamageEvent: HostId: {ctx.Host.UnitId} TargetId: {target.UnitId} Damage: {_damageAmount/2} SkillId: {_skillId} SkillRuntimeId: {_runtimeId}";
                    }
                    else
                    {
                        return EffectResult.Fail("EventBufferFull", "Failed to add SkillDamageEvent to EventBuffer.");
                    }
                }
            }
            return EffectResult.Ok(message);
        }
    }



}
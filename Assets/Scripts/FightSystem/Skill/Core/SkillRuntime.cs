//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.EventSystem.Events;
using MyFrame.EventSystem.Interfaces;
using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;
using System;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill.Core
{
    /// <summary>
    /// 检测条件：CheckAcess，外界传入条件上下文
    /// 选定目标：外界输入目标，内部运算处理得到技能中心点，依据配置AOE算法获得AOE目标列表
    /// 生成效果列表：依据技能效果配置，生成效果列表
    /// </summary>
    public class SkillRuntime : ISkill
    {
        
        protected readonly SkillInfo _skillInfo;
        public uint CurrentCD { get; set; } = 0;

        public Unit Host { get; }
        public SkillRuntime(SkillInfo skillInfo, Unit host)
        {
            _skillInfo = skillInfo;
            Host = host;
        }
        public virtual SkillCheckReport CheckAccess(in SkillConditionContxt ctx)
        {
            // Check Progress
            var result = _skillInfo.Condition.Check(ctx);
            bool canAccess = result.Success;
            string message = result.Code + "\nMessageKey: \n" + result.MessageKey + "\nArgs: \n" + result.Args;

            return new SkillCheckReport(canAccess, message);
        }
        public virtual SkillReport Execute(in SkillExecuteContext ctx)
        {
            var check_res = CheckAccess(ctx.ConditionContxt);
            if (!check_res.CanAccess)
            {
                return new SkillReport(false, "Skill: " + _skillInfo.SkillId + " Access Denied!\n" + check_res.Reason);
            }
            try
            {
                string message = String.Empty;  
                // 选定AOE目标,center为ctx中输入
                ITargetSelector targetSelector = _skillInfo.TargetSelector;
                var aoeTargets = targetSelector.Select(ctx.Center, Host);

                foreach(var t in aoeTargets)
                {
                    message += $"Target Selected: {t.UnitId}\n";
                }
                if (message == String.Empty)
                {
                    message = "No Target Selected!\n";
                }

                // 生成效果节点
                ISkillEffect effect = _skillInfo.Effect;

                //创建EventBuffer缓存效果事件
                var buffer = new EventBuffer(ctx.EventBus);

                // 生成效果上下文
                var effectCtx = new SkillEffectContext(Host, ctx.Center, aoeTargets,ctx.ConditionContxt , buffer);

                // 执行效果
                var report = effect.Execute(effectCtx);

                // 发布效果事件到事件总线
                buffer.Release();

                // 返回技能执行报告
                return new SkillReport(true, "Skill: " + _skillInfo.SkillId + " Execute Successfully!\n"+message + report.Message);
            }
            catch(Exception ex)
            {
                return new SkillReport(false, "Skill: " + _skillInfo.SkillId + " Execute Failed!\n" +  ex.Message);
            }
            
        }
    }

    
}

namespace MyFrame.FightSystem.Skill.Refs
{
    public readonly struct SkillExecuteContext
    {
        public readonly Unit Center;
        public readonly SkillConditionContxt ConditionContxt;
        public readonly IEventBusCore EventBus;

        public SkillExecuteContext(Unit center, SkillConditionContxt conditionContxt,IEventBusCore eventBus)
        {
            Center = center;
            ConditionContxt = conditionContxt;
            EventBus = eventBus;
        }
    }

    public readonly struct SkillEffectContext
    {
        public readonly Unit Host;
        public readonly Unit Center;
        public readonly IEnumerable<Unit> Targets;
        public readonly SkillConditionContxt ConditionContxt;
        
        public readonly EventBuffer EventBuffer;

        public readonly EffectExecuteTrace Trace;

        public SkillEffectContext(Unit host, Unit center, IEnumerable<Unit> targets, SkillConditionContxt conditionContxt, EventBuffer eventBuffer, EffectExecuteTrace? trace = null)
        {
            Host = host;
            Center = center;
            Targets = targets;
            ConditionContxt = conditionContxt;
            EventBuffer = eventBuffer;
            Trace = trace ?? new EffectExecuteTrace();
        }
    }
}
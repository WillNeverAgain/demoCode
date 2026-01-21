//Author : _SourceCode
//CreateTime : 2026-01-20-17:07:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.EventSystem.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.FightSystem.Skill.Core
{
    public class SkillPipeline
    {
        private IEventBusCore _eventBus;
        public SkillReport ExecuteSkill(SkillExecuteRequest request)
        {
            // 释放请求+
            SkillConditionContxt scctx = new SkillConditionContxt(request.Skill);
            // 检测条件
            var creport = request.Skill.CheckAccess(scctx);
            if (!creport.CanAccess) return new SkillReport(false, "Skill Access Denied!\n" + creport.Reason);
            // 执行
            var sreport = request.Skill.Execute(new SkillExecuteContext(request.Center, scctx,_eventBus));

            return sreport;
        }
    }

    public sealed record SkillExecuteRequest(SkillRuntime Skill , Unit Center);
}
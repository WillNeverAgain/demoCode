//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Refs;
using MyFrame.FightSystem.SkillV2.Refs;

namespace MyFrame.FightSystem.SkillV2.Runtime
{
    public sealed class SkillPipelineV2
    {
        private readonly SkillV2RuntimeContext _world;

        public SkillPipelineV2(SkillV2RuntimeContext world)
        {
            _world = world;
        }

        public SkillV2Report Execute(SkillRuntimeV2 skill, Unit center)
        {
            var req = new SkillV2ExecuteRequest(center);
            return skill.Execute(req, _world);
        }
    }
}

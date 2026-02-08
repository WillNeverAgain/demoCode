//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.EventSystem.Events;
using MyFrame.EventSystem.Interfaces;
using MyFrame.FightSystem.Skill.Refs;
using System;
using System.Collections.Generic;

namespace MyFrame.FightSystem.SkillV2.Refs
{
    public readonly struct SkillV2ExecuteRequest
    {
        public readonly Unit Center;

        public SkillV2ExecuteRequest(Unit center)
        {
            Center = center;
        }
    }

    public sealed class SkillV2RuntimeContext
    {
        public readonly IEventBusCore EventBus;
        public readonly CellMap Map;
        public readonly Random Rng;

        public SkillV2RuntimeContext(IEventBusCore eventBus, CellMap map, Random rng = null)
        {
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            Map = map ?? throw new ArgumentNullException(nameof(map));
            Rng = rng ?? new Random();
        }
    }

    /// <summary>
    /// Runtime context for predicates/expressions/actions during execution.
    /// </summary>
    public sealed class SkillV2EvalContext
    {
        public readonly string SkillId;
        public readonly uint SkillCurrentCd;
        public readonly Unit Host;
        public readonly Unit Center;

        public IReadOnlyList<Unit> PhaseTargets { get; internal set; }
        public Unit CurrentTarget { get; internal set; }

        public readonly CellMap Map;
        public readonly IEventBusCore EventBus;
        public readonly EventBuffer Buffer;
        public readonly Random Rng;

        public SkillV2EvalContext(string skillId, uint skillCurrentCd, Unit host, Unit center, IReadOnlyList<Unit> phaseTargets, Unit currentTarget,
            CellMap map, IEventBusCore eventBus, EventBuffer buffer, Random rng)
        {
            SkillId = skillId;
            SkillCurrentCd = skillCurrentCd;
            Host = host;
            Center = center;
            PhaseTargets = phaseTargets;
            CurrentTarget = currentTarget;
            Map = map;
            EventBus = eventBus;
            Buffer = buffer;
            Rng = rng;
        }
    }
}

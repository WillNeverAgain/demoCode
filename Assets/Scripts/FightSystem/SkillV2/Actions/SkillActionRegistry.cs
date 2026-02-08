//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.SkillV2.Configs;
using System;
using System.Collections.Generic;

namespace MyFrame.FightSystem.SkillV2.Actions
{
    /// <summary>
/// Action executor registry.
/// 
/// Design rule:
/// - All world-state mutations (damage/heal/buff/move) must be expressed as events buffered by Action executors.
/// - Skill/Phase/Stage/Effect/BT nodes must not directly mutate units.
/// </summary>
    public sealed class SkillActionRegistry
    {
        private readonly Dictionary<ActionType, ISkillActionExecutor> _map = new();

        /// <summary>Register/override an executor for an <see cref="ActionType"/>.</summary>
        public SkillActionRegistry Register(ISkillActionExecutor exec)
        {
            if (exec is null) throw new ArgumentNullException(nameof(exec));
            _map[exec.Type] = exec;
            return this;
        }

        public bool TryGet(ActionType type, out ISkillActionExecutor exec) => _map.TryGetValue(type, out exec);

        public static SkillActionRegistry CreateDefault()
        {
            return new SkillActionRegistry()
                .Register(new DamageActionExecutor())
                .Register(new HealActionExecutor())
                .Register(new AddBuffActionExecutor())
                .Register(new RemoveBuffActionExecutor())
                .Register(new MoveActionExecutor());
        }
    }
}

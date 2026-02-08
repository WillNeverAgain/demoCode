//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.EventSystem.Interfaces;
using MyFrame.FightSystem.Skill.Refs;

namespace MyFrame.EventSystem.Events
{
    public sealed record SkillHealEventV2(Unit Host, Unit Target, float HealAmount, string SkillId, string ActionId) : IEvent;

    public sealed record SkillAddBuffEventV2(Unit Host, Unit Target, string BuffId, int Stacks, string SkillId, string ActionId) : IEvent;

    public sealed record SkillRemoveBuffEventV2(Unit Host, Unit Target, string BuffId, int Stacks, string SkillId, string ActionId) : IEvent;

    public sealed record SkillMoveEventV2(Unit Host, Unit Target, int Dx, int Dy, string SkillId, string ActionId) : IEvent;
}

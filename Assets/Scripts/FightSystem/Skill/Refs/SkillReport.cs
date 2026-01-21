//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
namespace MyFrame.FightSystem.Skill.Refs
{
    /// <summary>
    /// Report generated after executing a skill.
    /// </summary>
    public record SkillReport(bool Success,string Message);

    public record EffectReport(string Message);
}
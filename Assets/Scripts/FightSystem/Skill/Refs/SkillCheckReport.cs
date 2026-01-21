//Author : _SourceCode
//CreateTime : 2026-01-14-17:17:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
#nullable enable
namespace MyFrame.FightSystem.Skill.Refs
{
    /// <summary>
    /// Report indicating whether a skill can be accessed and the reason if it cannot.
    /// </summary>
    /// <param name="CanAccess"></param>
    /// <param name="Reason"></param>
    public record SkillCheckReport(bool CanAccess, string Reason = "");
}
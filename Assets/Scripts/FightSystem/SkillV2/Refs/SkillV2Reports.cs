//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
namespace MyFrame.FightSystem.SkillV2.Refs
{
    public sealed record SkillV2CheckReport(bool CanAccess, string Reason);

    public sealed record SkillV2Report(bool Success, string Message);
}

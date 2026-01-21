//Author : _SourceCode
//CreateTime : 2026-01-14-12:45:36
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
namespace MyFrame.FightSystem.Calculator.Refs
{
    /// <summary>
    /// The Report Of A Value Calculation
    /// </summary>
    /// <param name="BaseValue">The Base Value</param>
    /// <param name="FinalValue">The Calculated Value</param>
    /// <param name="CalculationMessages">The Calculator Messages , Such as How The Value Changed</param>
    public record ValueReport(float BaseValue, float FinalValue, string CalculationMessages) { };
}
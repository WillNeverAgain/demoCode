//Author : _SourceCode
//CreateTime : 2026-01-14-12:45:36
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
using System.Collections.Generic;
using MyFrame.FightSystem.Calculator.Refs;
using MyFrame.FightSystem.Calculator.Core;
namespace MyFrame.FightSystem.Calculator.Interfaces
{
    public interface IValuCalculator
    {
        /// <summary>
        /// Calculate The Final Value Based On The Base Value And Modifiers
        /// </summary>
        /// <param name="value">The Base Vale</param>
        /// <param name="modifiers">The Modifiers To Modify The Value.</param>
        /// <returns></returns>
        public ValueReport Calculate(float value, IEnumerable<BaseValueModifier> modifiers);
    }
}
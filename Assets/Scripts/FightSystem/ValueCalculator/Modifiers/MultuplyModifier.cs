//Author : _SourceCode
//CreateTime : 2026-01-14-12:45:36
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
using MyFrame.FightSystem.Calculator.Core;
using MyFrame.FightSystem.Calculator.Refs;
namespace MyFrame.FightSystem.Calculator.Interfaces
{
    public class MultuplyModifier : BaseValueModifier
    {
        public override int Priority { get; }
        public override ValueCalculateStage Stage { get; }
        public override string Message { get; }
        private float modifyValue;
        /// <summary>
        /// Build a Multuply Modifier
        /// </summary>
        /// <param name="modifyValue">The Value To Multuply</param>
        /// <param name="stage">The Modify Stage To Multuply At. Must Be At "*** Multuply" Stage</param>
        /// <param name="priority">The Priority To Sort In which Stage. The Smaller it is , The higher it will be sorted . Most of Time won't be used</param>
        /// <param name="message">The Debug Message,Such as From Who.The Base Value Change Message Is Contained In Program</param>
        public MultuplyModifier(float modifyValue,ValueCalculateStage stage, int priority = 1, string message = null)
        {
            this.modifyValue = modifyValue;
            Stage = stage;
            Message = message;
            Priority = priority;
        }

        public override void Modify(ValueCalculateBreakPoint bd)
        {
            bd.CurrentValue *= modifyValue;
            if (Message is not null) bd.Message += ("\n" + Message);
            bd.Message += "\nMultiplied by " + modifyValue + " At Modify Stage " + Stage.ToString() + "\nCurrent Result Is " + bd.CurrentValue;
        }
    }
}
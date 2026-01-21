//Author : _SourceCode
//CreateTime : 2026-01-14-12:45:36
//Version : 0.1
//UnityVersion : 2022.3.62f1c1
using MyFrame.FightSystem.Calculator.Core;
using MyFrame.FightSystem.Calculator.Refs;
namespace MyFrame.FightSystem.Calculator.Interfaces
{
    /// <summary>
    /// Build a MaxValue Modifier
    /// </summary>
    /// <param name="modifyValue">The Value To Modify</param>
    /// <param name="stage">The Modify Stage To Modify At. Must Be At "*** Modify" Stage</param>
    /// <param name="priority">The Priority To Sort In which Stage. The Smaller it is , The higher it will be sorted . This May Be Important In Modify Stage</param>
    /// <param name="message">The Debug Message,Such as From Who.The Base Value Change Message Is Contained In Program</param>
    public class MaxValueModifier : BaseValueModifier
    {
        public override int Priority { get; }
        public override ValueCalculateStage Stage { get; }
        public override string Message { get; }
        private float modifyValue;

        public MaxValueModifier(float modifyValue, ValueCalculateStage stage, int priority = 1, string message = null)
        {
            this.modifyValue = modifyValue;
            Stage = stage;
            Message = message;
            Priority = priority;
        }

        public override void Modify(ValueCalculateBreakPoint bd)
        {
            if(bd.CurrentValue > modifyValue)
            {
                bd.CurrentValue = modifyValue;
                if (Message is not null) bd.Message += ("\n" + Message);
                bd.Message += ("\nClamp To " + modifyValue + " At Modfy Stage " + Stage.ToString() + "\nCurrent Result Is " + bd.CurrentValue);
            }
        }
    }
}
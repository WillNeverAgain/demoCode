//Author : _SourceCode
//CreateTime : 2025-08-12-03:37:10
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Calculator
{
    public interface IStatModifier
    {
        public StageStacking Stacking
        {
            get;
        }

        public ModifierStage Stage
        {
            get;
        }

        public string Source
        {
            get;
        }

        public bool applyTo(StatContext ctx);
        public float GetValue(StatContext ctx);
    }
}
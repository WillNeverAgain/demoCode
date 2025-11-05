//Author : _SourceCode
//CreateTime : 2025-08-13-02:24:49
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Calculator;

namespace MyFrame.FightSystem.Skill
{
    public interface IBaseDamageModPolicy
    {
        public ValueBreakdown Modify(ValueBreakdown bd,BaseDamageModContext ctx);
    }
}
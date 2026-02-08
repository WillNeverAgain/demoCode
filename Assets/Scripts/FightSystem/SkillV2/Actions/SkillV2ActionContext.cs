//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.SkillV2.Refs;

namespace MyFrame.FightSystem.SkillV2.Actions
{
    public readonly struct SkillV2ActionContext
    {
        public readonly SkillV2EvalContext Eval;
        public readonly string EffectId;
        public readonly string ActionId;

        public SkillV2ActionContext(SkillV2EvalContext eval, string effectId, string actionId)
        {
            Eval = eval;
            EffectId = effectId;
            ActionId = actionId;
        }
    }
}

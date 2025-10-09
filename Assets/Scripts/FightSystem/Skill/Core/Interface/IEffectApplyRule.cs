//Author : _SourceCode
//CreateTime : 2025-08-20-19:40:28
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Skill
{
    public interface IEffectApplyRule
    {
        public bool WhenApply(in SkillContext sctx, IBlackBoard blackBoard);
    }
}

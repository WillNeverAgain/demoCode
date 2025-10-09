//Author : _SourceCode
//CreateTime : 2025-08-20-19:38:41
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Skill
{
    public interface IEffect
    {
        public EffectReport Execute(SkillContext sctx , IBlackBoard blackBoard , ISkillTarget target);
        public EffectReport OnRemove(SkillContext sctx, IBlackBoard blackBoard, ISkillTarget target);
    }
}

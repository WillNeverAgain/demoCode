//Author : _SourceCode
//CreateTime : 2025-08-20-19:36:00
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


namespace MyFrame.FightSystem.Skill
{
    public interface ISkillGate
    {
        public bool Validate(in SkillContext sctx);
    }
}

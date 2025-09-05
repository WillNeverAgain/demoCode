//Author : _SourceCode
//CreateTime : 2025-08-20-19:34:44
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


using MyFrame.FightSystem.Unit;

namespace MyFrame.FightSystem.Skill
{
    public interface ISkillTarget : IFightObject
    {  
        public bool WhenSelected(SkillContext sctx);
        

    }

}
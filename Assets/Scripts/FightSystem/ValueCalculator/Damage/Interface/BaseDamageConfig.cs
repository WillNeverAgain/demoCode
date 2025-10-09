//Author : _SourceCode
//CreateTime : 2025-08-13-02:25:44
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem;
using MyFrame.FightSystem.Skill;

public class BaseDamageConfig
{
    public AttackType AttackType { get; init; }
    public ISkillExecuter SkillExecuter { get; init; }
    public ISkillTarget SkillTarget { get; init; }
    public float DamageRate { get; init; }
    public BaseDamageConfig(AttackType attackType, ISkillExecuter skillExecuter, ISkillTarget skillTarget, float damageRate)
    {
        AttackType = attackType;
        SkillExecuter = skillExecuter;
        SkillTarget = skillTarget;
        DamageRate = damageRate;
    }
}
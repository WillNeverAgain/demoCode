//Author : _SourceCode
//CreateTime : 2025-08-13-02:25:44
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem;

public interface IBaseDamageCalculator
{
    public float Compute(float final_attack,float final_defense,AttackType attack_type);
}

//Author : _SourceCode
//CreateTime : 2025-08-13-02:24:49
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem;
using System;

public class BaseDamageCalculator : IBaseDamageCalculator
{
    private const float MIN_ATTACK_RATE = 0.01F;
    public float Compute(float final_attack, float final_defense, AttackType attack_type)
    {
        return MathF.Max(final_attack - final_defense, final_attack * MIN_ATTACK_RATE);
    }
}

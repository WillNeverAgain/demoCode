using MyFrame.EventSystem.Core;
using MyFrame.FightSystem.Calculator.Core;
using MyFrame.FightSystem.Skill.Conditions;
using MyFrame.FightSystem.Skill.Core;
using MyFrame.FightSystem.Skill.Effects;
using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;
using MyFrame.FightSystem.Skill.TargetSelector;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("Test script is running.");
        EventBusCore eventBus = new();
        ValueCalculator valuCalculator = new();
        DamagePipeline damagePipeline = new(eventBus, valuCalculator);
        CellMap cellMap = new(10, 10);
        UnitPipeline unitPipeline = new(eventBus, cellMap);
        SkillPipeline skillPipeline = new(eventBus);

        List<Unit> list = new List<Unit>();
        for(int i = 0;i< 10; i++)
        {
            list.Add( new Unit(name: "Warrior" + i, unitId: i.ToString(), message: "", hP: 100, attack: 20, defence: 5, position: new UnitPosition(0, 0)));
        }
        for(int i = 0; i < list.Count; i++)
        {
            unitPipeline.CreateUnit(list[i], new UnitPosition(i % 5, i / 5));
        }

        string message = "";
        for(int i = 0; i < list.Count; i++)
        {
            message +=("Unit " + list[i].UnitId + " Position: (" + list[i].Position.X + ", " + list[i].Position.Y + ")\n");
        }
        Debug.Log(message);
        message = "";
        for (int i = 0; i < list.Count; i++)
        {
            message +=("Unit " + list[i].UnitId + " HP: " + list[i].HP +"\n");
        }
        Debug.Log(message);
        message = "";

        for (int i = 3; i < list.Count; i++)
        {
            list[i].SetType(EffectTargetType.Target);
        }

        SkillEffectInfo skillEffectInfo1 = new SkillEffectInfo("DmageEffect","1",EffectTargetType.Target,null,null);

        SkillInfo skill1Info = new SkillInfo( skillId: "SK001", skillName: "Fireball", skillDesc: "Damage 10",cD: 2,0, condition: new SkillCDCondition(), targetSelector: new AoeTargetSelector(radius: 2,cellMap), effect: new SkillDamageEffect(skillEffectInfo1,"1","1",null,10));

        SkillRuntime skill1 = new SkillRuntime(skill1Info, list[0]);

        new MuliplyDamageBuff(5, 0, false).Apply(list[0]);

        var report = skillPipeline.ExecuteSkill(new SkillExecuteRequest(skill1, list[3]));
        message += "Skill Executed\n";
        message += "Skill: " + skill1Info.SkillName + " Id: " + skill1Info.SkillId + "\nTargetSelector: Center & Center From (X +- r,Y +- r) \n SkillEffect: DamageEffect 10" + "\n";
        message += "Unit Base : \nHP : 100    Defence : 5" ;
        message += "\nSkill Host : unit0 , Target : unit3 , SkillTargetType : Target";
        message += "Attacker Buff: \nMuliplyDamageBuff x5\n";
        message += "\nAfter Skill Execution:\n";
        for (int i = 0 ; i < list.Count ; i++)
        {
            message += ($"unit{i}:\nHP:{list[i].HP}\nPos: ({list[i].Position.X},{list[i].Position.Y})\nMessage: \n" + list[i].Message + "\n");
        }
        Debug.Log(message);
        Debug.Log(report.Message);
    }
}


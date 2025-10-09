//Author : _SourceCode
//CreateTime : 2025-08-22-14:18:37
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Unit;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill
{
    public class Skill : ISkill
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Id { get; private set; }
        public IEnumerable<ISkillCost> Cost { get; private set; }
        public IEnumerable<ISkillGate> Gate { get; private set; }
        public List<EffectRule> Rules { get; private set; }
        public SelectorQuery Query { get; private set; }
        private IWordQuery wordQuery;
        public ISkillTargetSelector TargetSelector { get; private set; }

        public SkillReport Execute(in SkillContext sctx, TargetPos aim)
        {
            List<ISkillTarget> targets = TargetSelector.Select(Query, wordQuery);
            SkillReport skillReport = new SkillReport();
            DicBlackBoard board = new DicBlackBoard();
            foreach (ISkillGate gate in Gate)
            {
                if (!gate.Validate(in sctx))
                {
                    skillReport.Notes.Add("Failed to execute Skill : " + Name + "because skill gates validate failed");
                    return skillReport;
                }
            }
            foreach (ISkillCost cost in Cost)
            {
                if (!cost.Cost(sctx))
                {
                    skillReport.Notes.Add("Failed to execute Skill : " + Name + "because skill cost is not enough");
                    return skillReport;
                }
            }
            foreach (EffectRule rule in Rules)
            {
                bool apply = true;
                foreach (IEffectApplyRule apply_rule in rule.ApplyRule)
                {
                    apply = apply & apply_rule.WhenApply(sctx, board);
                }
                if (!apply) { continue; }
                foreach (ISkillTarget target in targets)
                {
                    if (rule.TargetTypes.Contains(target.TargetType))
                    {
                        rule.Effect.Execute(sctx, board, target);
                    }
                }

            }
            return skillReport;
        }
    }
}

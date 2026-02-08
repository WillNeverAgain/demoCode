//Author : _SourceCode
//CreateTime : 2026-02-08
//Version : 2.0
//UnityVersion : 2022.3.62f1c1
#nullable enable
using MyFrame.FightSystem.Skill.Refs;
using MyFrame.FightSystem.SkillV2.Configs;
using MyFrame.FightSystem.SkillV2.Runtime;
using System.Collections.Generic;

namespace MyFrame.FightSystem.SkillV2.Targeting
{
    /// <summary>
/// Executes a table-driven target selector (<see cref="Configs.TargetSelectorDef"/>).
/// 
/// Selector decides the target set for:
/// - Phase default targets
/// - Effect custom selector (when targetSource=CustomSelector)
/// 
/// NOTE:
/// This module should be *pure* (selection only). Do not mutate world state here.
/// </summary>
    public static class TargetSelectorExecutor
    {
        public static List<Unit> Select(TargetSelectorDef def, Unit center, CellMap map)
        {
            var res = new List<Unit>();
            if (def is null || map is null || center is null) return res;

            switch (def.type)
            {
                case TargetSelectorType.AoeSquare:
                    SelectAoeSquare(def, center, map, res);
                    break;

                case TargetSelectorType.AoeDiamond:
                    SelectAoeDiamond(def, center, map, res);
                    break;

                case TargetSelectorType.None:
                default:
                    break;
            }

            // Optional include center
            if (def.includeCenter && SkillV2Util.IsValidTarget(center) && !Contains(res, center))
                res.Add(center);

            // Filter by effectTargetMask if needed
            if (def.effectTargetMask != 0)
            {
                for (int i = res.Count - 1; i >= 0; i--)
                {
                    var u = res[i];
                    if (u is null) { res.RemoveAt(i); continue; }
                    if (((int)u.EffectTargetType & def.effectTargetMask) == 0) res.RemoveAt(i);
                }
            }

            // Remove invalids
            for (int i = res.Count - 1; i >= 0; i--)
                if (!SkillV2Util.IsValidTarget(res[i])) res.RemoveAt(i);

            return res;
        }

        private static void SelectAoeSquare(TargetSelectorDef def, Unit center, CellMap map, List<Unit> res)
        {
            var r = def.radius;
            var pos = center.Position;
            for (int dx = -r; dx <= r; dx++)
            for (int dy = -r; dy <= r; dy++)
            {
                var cell = map.GetCell(new UnitPosition(pos.X + dx, pos.Y + dy));
                if (cell?.OccupiedUnits is null) continue;
                foreach (var u in cell.OccupiedUnits)
                {
                    if (u is null) continue;
                    if (!def.includeCenter && u.UnitId == center.UnitId) continue;
                    if (SkillV2Util.IsValidTarget(u) && !Contains(res, u)) res.Add(u);
                }
            }
        }

        private static void SelectAoeDiamond(TargetSelectorDef def, Unit center, CellMap map, List<Unit> res)
        {
            var r = def.radius;
            var pos = center.Position;
            for (int dx = -r; dx <= r; dx++)
            for (int dy = -r; dy <= r; dy++)
            {
                if (System.Math.Abs(dx) + System.Math.Abs(dy) > r) continue;
                var cell = map.GetCell(new UnitPosition(pos.X + dx, pos.Y + dy));
                if (cell?.OccupiedUnits is null) continue;
                foreach (var u in cell.OccupiedUnits)
                {
                    if (u is null) continue;
                    if (!def.includeCenter && u.UnitId == center.UnitId) continue;
                    if (SkillV2Util.IsValidTarget(u) && !Contains(res, u)) res.Add(u);
                }
            }
        }

        private static bool Contains(List<Unit> list, Unit u)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i]?.UnitId == u.UnitId) return true;
            return false;
        }
    }
}

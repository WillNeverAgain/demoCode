//Author: _SourceCode
//CreateTime : 2026-01-21-23:20:38
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using MyFrame.FightSystem.Skill.Interfaces;
using MyFrame.FightSystem.Skill.Refs;
using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill.TargetSelector
{
    public class AoeTargetSelector : ITargetSelector
    {
        private readonly int _radius;
        private CellMap _map;
        public AoeTargetSelector(int radius, CellMap map)
        {
            _radius = radius;
            _map = map;
        }
        public IEnumerable<Unit> Select(Unit center, Unit host)
        {
            UnitPosition pos = center.Position;
            List<Unit> targets = new List<Unit>();
            for (int i = -_radius; i <= _radius; i++)
            {
                for (int j = -_radius; j <= _radius; j++)
                {
                    UnitPosition targetPos = new UnitPosition(pos.X + i, pos.Y + j);
                    UnitCell cell = _map.GetCell(targetPos);
                    if (cell != null && cell.OccupiedUnits != null)
                    {
                        foreach (Unit targetUnit in cell.OccupiedUnits)
                        {
                            if (targetUnit.UnitId != center.UnitId)
                            {
                                targets.Add(targetUnit);
                            }
                        }
                    }
                }
            }
            return targets;
        }
    }
}
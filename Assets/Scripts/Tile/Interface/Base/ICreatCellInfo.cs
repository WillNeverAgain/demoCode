using System.Collections.Generic;
using UnityEngine;
namespace Tile.Interface.Base
{
    public interface ICreateCellInfo
    {
        public Sprite Sprite{get;}
        public string CellName { get; }
        public int TileLayer{get;}
        public IReadOnlyList<object> args{get;}
    }
}
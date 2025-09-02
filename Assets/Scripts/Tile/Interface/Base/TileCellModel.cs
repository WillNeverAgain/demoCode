using System;
using System.Collections.Generic;
using UnityEngine;
namespace Tile.Interface.Base
{
    [Serializable]
    public class TileCellModel
    {
        public Vector2Int position;
        public string Name;
        public List<string> args;
    }
}
using System;
using UnityEngine;
namespace ODG.Process.Dungeon
{
    public abstract class DungeonTarget : MonoBehaviour
    {
        public string TargetDescription => GetDescription();
        
        [SerializeField]
        private string _description;
        public string GetDescription()
        {
            return _description.Equals(string.Empty)? "DefaultDes" :_description;
        }
    }
}
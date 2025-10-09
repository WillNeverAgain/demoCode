//Author : _SourceCode
//CreateTime : 2025-09-10-14:56:29
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System;
using System.Collections.Generic;
#nullable enable
namespace MyFrame.FightSystem.Buff
{
    public class BufIdGuid : IBuffId
    {
        private Dictionary<IBuff,Guid> _dictionary = new();
        public string? GetId(IBuff buff)
        {
            if(buff == null) return null;
            if(_dictionary.TryGetValue(buff, out var id)) return id.ToString();
            return null;
        }
        public string GenerateId(IBuff buff)
        {
            Guid guid = Guid.NewGuid();
            _dictionary.Add(buff, guid);
            return guid.ToString();
        }
    }
}

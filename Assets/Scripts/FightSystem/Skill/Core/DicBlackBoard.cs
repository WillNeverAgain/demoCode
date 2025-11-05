//Author : _SourceCode
//CreateTime : 2025-08-22-14:39:16
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

using System.Collections.Generic;

namespace MyFrame.FightSystem.Skill
{
    public class DicBlackBoard : IBlackBoard
    {
        private Dictionary<string, object> dictionary = new Dictionary<string, object>();
        public T Get<T>(string key, T default_value = default)
        {
            if(!dictionary.TryGetValue(key, out object value))
            {
                value = default_value;
            }
            return (T)value;
        }

        public void Set<T>(string key, T value)
        {
            if(dictionary.ContainsKey(key))
            {
                return;
            }
            dictionary.Add(key, value);
        }
    }
}

//Author : _SourceCode
//CreateTime : 2025-08-20-20:08:41
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

namespace MyFrame.FightSystem.Skill
{
    public interface IBlackBoard
    {
        public T Get<T>(string key, T default_value = default);
        public void Set<T>(string key, T value);
    }
}

//Author : _SourceCode
//CreateTime : 2025-09-10-14:16:19
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

public interface IEvent
{

}

public interface IFightSceneEvent : IEvent
{
    public string Name {  get; }
    public string Source {  get; }
}

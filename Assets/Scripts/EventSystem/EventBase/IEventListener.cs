//Author : _SourceCode
//CreateTime : 2025-08-08-13:41:43
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


using System;
public interface IEventListener
{
    Type HandleType { get; }
    void onEventObject(object evt);

}

public interface IEventListener<T>:IEventListener
{
    void OnEvent(T evt);
}

public abstract class MyEventListener<T> : IEventListener<T> where T : MyEventBase
{
    public Type HandleType => typeof(T);

    abstract public void OnEvent(T evt);

    public void onEventObject(object evt)
    {
        if (evt is T t) OnEvent(t);
        else throw new InvalidCastException(
               $"The type of event parameter is {evt?.GetType().FullName ?? "null"}£¬What is needed for the listener {typeof(T).FullName} is not matched.");
    }
}

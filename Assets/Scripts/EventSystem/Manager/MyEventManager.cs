//Author : _SourceCode
//CreateTime : 2025-08-08-14:50:25
//Version : 0.1
//UnityVersion : 2022.3.62f1c1



public class MyEventManager
{
    private static readonly EventMessenger<MyEventType> messenger;
    private MyEventManager() { }

    private static MyEventManager instance = new MyEventManager();
    public static MyEventManager Instance {  get { return instance; } }

    public void registerListener(MyEventType key, IEventListener listener)
    {
        messenger.registerListener(key, listener);
    }
    public void removeListener(MyEventType key, IEventListener listener)
    {
        messenger.removeListener(key, listener);
    }
    public void removeListener(MyEventType key)
    {
        messenger.removeListenersByKey(key);
    }
    public void broadcast<T>(MyEventType key, T args)
    {
        messenger.broadcast(key, args);
    }
}

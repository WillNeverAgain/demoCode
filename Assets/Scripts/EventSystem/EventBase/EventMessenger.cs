//Author : _SourceCode
//CreateTime : 2025-08-08-13:50:39
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class EventMessenger<T>
{
    private Dictionary<T, List<IEventListener>> eventTable = new Dictionary<T, List<IEventListener>>();
    private Dictionary<T, Type> keyTypeMap = new Dictionary<T, Type>();
    private readonly object _lock = new();

    private void onListenerAdding(T eventType, IEventListener listener)
    {
        if (listener == null) throw new ListenerException("you tried to add a null listener: " + nameof(listener));
        Type type = listener.HandleType;
        if (type == null) throw new ListenerException("the listener you tried to add is not invilid because the HandleType of it is null");
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable.Add(eventType, new List<IEventListener>());
            keyTypeMap.Add(eventType, type);
        }
        else
        {
            Type existingType = keyTypeMap[eventType];
            if (!existingType.Equals(type))
            {
                throw new ListenerException("Attempting to add listener with inconsistent signature for event type " + type +
                        "Current listeners have type " + existingType + " and listener being added has type " + type);
            }
        }

    }

    private void onListenerRemoving(T eventType, IEventListener listener)
    {
        if (listener == null) throw new ListenerException("the listener you tried to remove is null");
        if (!eventTable.TryGetValue(eventType, out List<IEventListener> value)) throw new ListenerException("Attempting to remove listener for type " + eventType + " but Messenger doesn't know about this event type.");
        if (value == null) throw new ListenerException("Attempting to remove listener with for event type " + eventType + " but current listener is null.");
        if (!value.Contains(listener)) throw new ListenerException("Attempting to remove listener with for event type " + eventType + " but listener is not contained.");
        else
        {
            if (!keyTypeMap[eventType].Equals(listener.HandleType))
            {
                throw new ListenerException("Attempting to remove listener with inconsistent signature for event type " + eventType + ". " +
                        " Current listeners have type " + keyTypeMap[eventType] + " and listener being removed has type " + listener.HandleType);
            }
        }
    }

    private void onListenerRemoving(T eventType)
    {
        if (!eventTable.ContainsKey(eventType))
        {
            throw new ListenerException("Attempting to remove listener for type " + eventType + " but Messenger doesn't know about this event type.");
        }
    }

    private void onListenerRemoved(T eventType)
    {
        if (!eventTable.TryGetValue(eventType, out List<IEventListener> value)) throw new ListenerException("when this exception is thrown,you might have to think about whther your code has some bugs?");
        if(value == null||value.Count == 0)
        {
            eventTable.Remove(eventType);
            keyTypeMap.Remove(eventType);
        }
    }

    private void onBroadcasting(T eventType)
    {
        if (!eventTable.ContainsKey(eventType)) throw new BroadcastException("Broadcasting message " + eventTable + " but no listener found.");
    }

    public void registerListener(T eventType,IEventListener eventListener)
    {
        onListenerAdding(eventType,eventListener);
        lock (_lock)
        {
            eventTable[eventType].Add(eventListener);
        }

    }
    public void removeListenersByKey(T eventType)
    {
        onListenerRemoving(eventType);
        eventTable.Remove(eventType);
        keyTypeMap.Remove(eventType);
    }

    public void removeListener(T eventType,IEventListener eventListener)
    {
        onListenerRemoving(eventType,eventListener);
        eventTable[eventType].Remove(eventListener);
        onListenerRemoved(eventType);
    }

    public void broadcast<K>(T eventType,K args)
    {
        List<IEventListener> snapshot;
        Type expectedType;
        lock (_lock)
        {
            onBroadcasting(eventType);
            expectedType = keyTypeMap[eventType];
            snapshot = new List<IEventListener>(eventTable[eventType]);
        }
        if (args == null) throw new BroadcastException("The event args tou tried to broadcast is null");
        if (!typeof(K).Equals(expectedType) ) throw new BroadcastException("Broadcast argument type mismatch for key: " + eventType +". Expected: " + expectedType.Name + ", but got: " + args.GetType().Name);
        foreach (var l in snapshot)
        {
            try
            {
                l.onEventObject(args!);
            }
            catch (Exception ex)
            {
                throw new BroadcastException($"Broadcast to listener {l.GetType().FullName} failed: {ex.Message}");
            }
        }
    }
}

[Serializable]
internal class BroadcastException : Exception
{
    public BroadcastException()
    {
    }

    public BroadcastException(string message) : base(message)
    {
    }

    public BroadcastException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected BroadcastException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
internal class ListenerException : Exception
{
    public ListenerException()
    {
    }

    public ListenerException(string message) : base(message)
    {
    }

    public ListenerException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ListenerException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
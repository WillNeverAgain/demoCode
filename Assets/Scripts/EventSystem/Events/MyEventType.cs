//Author : _SourceCode
//CreateTime : 2025-08-08-14:57:27
//Version : 0.1
//UnityVersion : 2022.3.62f1c1


public sealed class MyEventType
{
    public string Name { get; }
    public int Code { get; }

    private MyEventType(string name, int code)
    {
        Name = name;
        Code = code;
    }
    public override string ToString() => Name;

    public static readonly MyEventType EmptyEvent = new("EmptyEvent", 0);

}

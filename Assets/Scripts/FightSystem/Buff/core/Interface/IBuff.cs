//Author : _SourceCode
//CreateTime : 2025-09-05-14:57:10
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

public interface IBuff
{
    public BuffReport OnUpdate();
    public BuffReport OnRemove();
    public BuffReport OnReceive();
}

public class BuffReport
{
}
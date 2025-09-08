//Author : _SourceCode
//CreateTime : 2025-09-08-08:47:11
//Version : 0.1
//UnityVersion : 2022.3.62f1c1

public interface IBuff
{
    public IBuffId Id { get; }
    public string Name { get; }
    public IBuffStack Stack { get; }
    public IBuffOwner Owner { get; }
    public IBuffOwner Source {  get; }
}
public interface IBuffOwner
{

}
public interface IBuffId
{
    public long GetId();
}
public interface IBuffStack
{
    public int GetCount();
    public bool AddStack(int value);
    public bool SetStack(int value);
}
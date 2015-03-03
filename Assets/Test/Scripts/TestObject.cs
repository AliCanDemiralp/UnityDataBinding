using System;

[Serializable]
public class TestObject 
{
    public enum Type
    {
        Type1,
        Type2,
        Type3,
        Type4
    }

    public string   Name;
    public Type     ObjectType;
    public int      ObjectInteger;
}

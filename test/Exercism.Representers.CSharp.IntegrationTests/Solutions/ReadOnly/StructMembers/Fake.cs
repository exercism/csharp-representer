using System;

public struct Fake
{
    private int p;

    public readonly int Get()
    {
        return p;
    }

    public readonly int P
    {
        get => p;
    }
    
    public int P2
    {
        readonly get => p;
        set { p = value; }
    }
}

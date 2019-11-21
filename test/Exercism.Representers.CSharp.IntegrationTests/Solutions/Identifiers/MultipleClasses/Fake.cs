using System;

public static class Fake
{
    public static int Foo(int input)
    {
        return input;
    }
    
    public static float Bar(bool enabled, float precision)
    {
        return enabled ? precision : precision * 2.0;
    }
}

public static class AnotherFake
{
    public static int Foo(int input)
    {
        return input;
    }
    
    public static float Fake(bool doIt, float precision)
    {
        return doIt ? precision : precision * 2.0;
    }
}
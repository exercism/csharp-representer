using System;

public static class Fake
{
    public static string Test(string str)
    {
        string otherStr = str + "test";
        return otherStr + "123";
    }
}
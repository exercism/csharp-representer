using System;

public static class Fake
{
    public static System.String Test(System.String str)
    {
        System.String otherStr = str + "test";
        return otherStr + "123";
    }
}
using System;

public static class Fake
{
    public static String Test(String str)
    {
        String otherStr = str + "test";
        return otherStr + "123";
    }
}
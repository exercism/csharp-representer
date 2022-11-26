using System;

public static class Fake
{
    public static int Test(int input, int expected)
    {
        int Add(int x, int y) => x + y;
        
        return Add(input, expected);
    }
}
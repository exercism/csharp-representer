using System;

public static class Fake
{
    public static int Test()
    {
        if (1 > 2)
            if (1 > 3)
                if (4 < 5)
                    return 0;
                else
                    return 2;

        return 1;
    }
}
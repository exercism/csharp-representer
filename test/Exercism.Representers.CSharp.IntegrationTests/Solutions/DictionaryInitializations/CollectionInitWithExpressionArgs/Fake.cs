using System;
using System.Collections.Generic;
using System.Composition;

public class Fake
{
    private const int nn = 5;
    Dictionary<string, int> dict = new Dictionary<string, int>
    {
        {
            "System.DateTime.Now is " + $"{System.DateTime.Now}",
            (5 * 10) / 3 + nn
        }
    };

}
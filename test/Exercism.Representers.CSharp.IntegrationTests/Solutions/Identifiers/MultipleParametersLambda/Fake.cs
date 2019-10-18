using System;
using System.Collections.Generic;
using System.Linq;

public static class Fake
{
    public static IEnumerable<int> Test(IEnumerable<int> numbers)
    {   
        return numbers.Select((x, i) => x > i);
    }
}
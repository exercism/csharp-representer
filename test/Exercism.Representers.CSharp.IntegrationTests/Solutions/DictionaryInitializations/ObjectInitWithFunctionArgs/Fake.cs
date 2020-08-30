using System;
using System.Collections.Generic;

public class Fake
{
    void Foo()
    {
        int LocalInt(int nn) => nn;
        var dict = new Dictionary<int, Func<string, string>> {[LocalInt(5)] = s => s};

    }
}
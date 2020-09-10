using System;
using System.Collections.Generic;
using System.Reflection;

public class Fake
{
    Dictionary<List<int>, List<string>> dict = new Dictionary<List<int>, List<string>>
        {[new List<int> {1}] = new List<string> {"one"}};
}
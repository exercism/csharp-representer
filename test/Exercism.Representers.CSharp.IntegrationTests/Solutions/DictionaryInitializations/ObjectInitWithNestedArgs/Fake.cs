using System;
using System.Collections.Generic;
using System.Reflection;

public class Fake
{
    Dictionary<Dictionary<long, DateTime>, Dictionary<byte, short>> dict 
        = new Dictionary<Dictionary<long, DateTime>, Dictionary<byte, short>>
        {[new Dictionary<long, DateTime> {[1L] = DateTime.Now}] =
            new Dictionary<byte,short> {[42] = 1729}};
}
using System.Collections.Generic;
using System.Reflection;

public class Fake
{
    private int field1 = 1;
    private string field2 = "no";
    public void Test()
    {
        var nn = 1;
        var ss = "one";
        var dict = new Dictionary<int, string>
        {
            [nn] = ss,
            [field1] = "hello",
            [nn] = "literally",
            [1] = ss,
            [1] = field2,
        };
    }
}
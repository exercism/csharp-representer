using System;
using System.Linq;

public static class Fake
{
    public static int Foo(int input, int expected)
    {
        int Add(int x, int y) => x + y;
        
        return Add(input, expected);
    }
    
    public static float Bar(bool enabled, float precision)
    {
        var x = new int[] { 1, 2, 3, 4 };
        var y = numbers.Where(x => x > 0).Concat(numbers.Where(x => x > 2)).Where((x, i) => x + i > 2).Select(y => y * 3)
            
        var z = numbers.Select((x, i) => x > i);    

        return enabled ? precision & z.Count() : precision * numbers.Count();
    }
    
    public static int Baz(int expected)
    {
        return Enumerable.Range(1, expected).Select(i => i * i)
    }
}
using System;
using System.Collections.Generic;

public class Fake
{
    private Dictionary<object, object> dict 
        = new Dictionary<object, object>
        {
            {(Func<Dictionary<object, object>, Dictionary<object, object>>)(d =>
            {
                d = new Dictionary<object, object>
                {
                    {null, new Dictionary<int, string> {{1, "one"}, {2, "two"}}}
                };
                return d;
            }), (Func<int, int>)(s => s)},
            {DateTime.Now, (Func<int, int>)(s => s)},
            {DateTime.Now, (Func<int, int>)(s => s)},
        };

}

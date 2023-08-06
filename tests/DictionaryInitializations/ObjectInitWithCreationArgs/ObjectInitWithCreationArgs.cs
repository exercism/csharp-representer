using System;
using System.Collections.Generic;

public class Fake
{
    Dictionary<DateTime, Random> dict = new Dictionary<DateTime, Random> {[System.DateTime.Now] = new Random()};
}
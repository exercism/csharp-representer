using System;

public class Fake
{
    private TimeSpan ts;
    public ref readonly TimeSpan GetTimeSpan() => ref ts;

    void Foo()
    {
        ref readonly TimeSpan ts2 = ref GetTimeSpan();
    }
}

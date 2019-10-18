public static class Fake
{
    public static int Test(int number)
    {
        var a = number + 2;
        var b = a % 5;
        var c = b + a + number;
        return c / 2;
    }
}
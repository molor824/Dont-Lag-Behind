public static class Ease
{
    // from easings.net
    // return x < 0.5 ? 2 * x * x : 1 - pow(-2 * x + 2, 2) / 2;
    public static float InOutQuad(float x)
    {
        var x1 = -2 * x + 2;

        return x < 0.5f ? 2 * x * x : 1 - x1 * x1 / 2;
    }
    public static float InQuart(float x)
    {
        return x * x * x * x;
    }
    public static float OutQuart(float x)
    {
        var x1 = 1 - x;

        return 1 - x1 * x1 * x1 * x1;
    }
}
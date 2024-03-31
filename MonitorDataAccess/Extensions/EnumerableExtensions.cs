namespace MonitorDataAccess.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<TSource> TakeEveryOddNumbered<TSource>(this IEnumerable<TSource> source)
    {
        bool odd = true;
        foreach (var item in source)
        {
            if (odd)
            {
                yield return item;
            }
            odd = !odd;
        }
    }

    public static IEnumerable<TSource> TakeEveryEvenNumbered<TSource>(this IEnumerable<TSource> source)
    {
        bool even = false;
        foreach (var item in source)
        {
            if (even)
            {
                yield return item;
            }
            even = !even;
        }
    }
}

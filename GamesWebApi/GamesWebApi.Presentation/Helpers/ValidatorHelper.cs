using System.Collections;

namespace GamesWebApi.Helpers;

public static class ValidatorHelper
{
    public static bool IsGreaterThanXAndLessThanY<T>(this ICollection<T> collection, int x, int y)
    {
        return collection.Count > x && collection.Count < y;
    }

    public static bool NotHasDuplicates<T>(this ICollection<T> collection)
    {
        return collection.Count == collection.GroupBy(x => x).Count();
    }

    public static bool IsAValidDate(this string date)
    {
        return DateTime.TryParse(date, out _);
    }
}
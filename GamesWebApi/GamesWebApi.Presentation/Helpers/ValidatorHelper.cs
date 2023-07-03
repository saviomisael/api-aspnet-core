namespace GamesWebApi.Helpers;

public static class ValidatorHelper
{
    public static bool IsGreaterThanXAndLessThanY<T>(this ICollection<T> collection, int x, int y) => collection.Count > x && collection.Count < y;

    public static bool NotHasDuplicates<T>(this ICollection<T> collection) => collection.Count == collection.GroupBy(x => x).Count();

    public static bool IsAValidDate(this string date) => DateTime.TryParse(date, out _);

    public static bool IsImageSizeLessThanOrEqualTo(long size, long x)
    {
        return size <= x;
    }

    public static bool IsImageTypeSupported(string imageType, string[] validImageTypes)
    {
        return validImageTypes.Contains(imageType);
    }
}
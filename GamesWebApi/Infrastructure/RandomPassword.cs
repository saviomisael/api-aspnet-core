namespace Infrastructure;

public class RandomPassword
{
    public static string Generate()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        var lowercases = new[]
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
            "v", "w", "x", "y", "z"
        };
        var uppercases = new[]
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
            "V", "W", "X", "Y", "Z"
        };
        var nonAlphanumericals = new[] { "@", "$", "!", "%", "#", "*", "?", "&" };
        var password = "";

        password += lowercases[new Random().Next(lowercases.Length)];
        password += uppercases[new Random().Next(uppercases.Length)];
        password += nonAlphanumericals[new Random().Next(nonAlphanumericals.Length)];

        while (password.Length < 8)
        {
            password += numbers[new Random().Next(numbers.Length)];
        }

        return password;
    }
}
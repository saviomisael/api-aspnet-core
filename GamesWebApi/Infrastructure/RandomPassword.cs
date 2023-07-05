namespace Infrastructure;

public static class RandomPassword
{
    public static string Generate()
    {
        var password = "";

        var charsRequired = new List<string>
        {
            GenerateRandomLowercase(),
            GenerateRandomUppercase(),
            GenerateRandomNonAlphanumerical(),
            GenerateRandomNumber()
        };

        while (charsRequired.Count > 0)
        {
            var randomChar = charsRequired[new Random().Next(charsRequired.Count)];
            password += randomChar;
            charsRequired.Remove(randomChar);
        }

        while (password.Length < 8)
        {
            var randomChars = new[]
            {
                GenerateRandomLowercase(), GenerateRandomUppercase(), GenerateRandomNonAlphanumerical(),
                GenerateRandomNumber()
            };
            password += randomChars[new Random().Next(randomChars.Length)];
        }

        return password;
    }

    private static string GenerateRandomNumber()
    {
        var numbers = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        return numbers[new Random().Next(numbers.Length)];
    }

    private static string GenerateRandomLowercase()
    {
        var lowercases = new[]
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
            "v", "w", "x", "y", "z"
        };

        return lowercases[new Random().Next(lowercases.Length)];
    }

    private static string GenerateRandomUppercase()
    {
        var uppercases = new[]
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
            "V", "W", "X", "Y", "Z"
        };

        return uppercases[new Random().Next(uppercases.Length)];
    }

    private static string GenerateRandomNonAlphanumerical()
    {
        var nonAlphanumericals = new[] { "@", "$", "!", "%", "#", "*", "?", "&" };
        return nonAlphanumericals[new Random().Next(nonAlphanumericals.Length)];
    }
}
namespace Infrastructure;

public class RandomPassword
{
    public static string Generate()
    {
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        var password = "";

        while (password.Length < 8)
        {
            password += numbers[new Random().Next(numbers.Length)];
        }
        
        return password;
    }
}
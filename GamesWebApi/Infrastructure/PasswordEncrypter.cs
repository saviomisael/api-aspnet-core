namespace Infrastructure;

public static class PasswordEncrypter
{
    public static string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool Compare(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
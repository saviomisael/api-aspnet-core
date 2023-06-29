namespace Application.Exception;

public class PlatformAlreadyExistsException : System.Exception
{
    public PlatformAlreadyExistsException(string platform) : base($"Platform {platform} already exists.")
    {
    }
}
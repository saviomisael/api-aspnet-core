namespace Application.Exception;

public class PlatformNotFoundException : System.Exception
{
    public PlatformNotFoundException(string platform) : base($"Platform {platform} not found.")
    {
        
    }

    public PlatformNotFoundException() : base("Platform not found.")
    {
        
    }
}
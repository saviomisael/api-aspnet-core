namespace Application.Exception;

public class PlatformHasRelatedGamesException : System.Exception
{
    public PlatformHasRelatedGamesException(string name) : base($"Games are using platform {name}.")
    {
        
    }
}
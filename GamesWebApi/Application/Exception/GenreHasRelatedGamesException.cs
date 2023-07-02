namespace Application.Exception;

public class GenreHasRelatedGamesException : System.Exception
{
    public GenreHasRelatedGamesException(string name) : base($"Games are using genre {name}.")
    {
        
    }
}
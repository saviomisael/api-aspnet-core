namespace Application.Exception;

public class GenreNotFoundException : System.Exception
{
    public GenreNotFoundException(string genre) : base($"Genre {genre} not found.")
    {
    }
}
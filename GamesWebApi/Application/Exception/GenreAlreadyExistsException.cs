namespace Application.Exception;

public class GenreAlreadyExistsException : System.Exception
{
    public GenreAlreadyExistsException(string genre) : base($"Genre {genre} already exists.")
    {
    }
}
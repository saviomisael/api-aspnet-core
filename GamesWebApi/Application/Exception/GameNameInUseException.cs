namespace Application.Exception;

public class GameNameInUseException : System.Exception
{
    public GameNameInUseException(string gameName) : base($"Game {gameName} already exists.")
    { }
}
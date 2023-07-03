namespace Application.Exception;

public class GameNotFoundException : System.Exception
{
    public GameNotFoundException() : base("Game not found.")
    {
        
    }
}
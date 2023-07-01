namespace Application.Exception;

public class AgeNotFoundException : System.Exception
{
    public AgeNotFoundException() : base("Age rating not found.")
    {
        
    }
}
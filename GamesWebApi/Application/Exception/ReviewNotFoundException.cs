namespace Application.Exception;

public class ReviewNotFoundException : System.Exception
{
    public ReviewNotFoundException() : base("Review not found.")
    {
        
    }
}
namespace Application.Exception;

public class ReviewerNotFoundException : System.Exception
{
    public ReviewerNotFoundException(string username) : base($"Reviewer {username} not found.")
    {
        
    }
}
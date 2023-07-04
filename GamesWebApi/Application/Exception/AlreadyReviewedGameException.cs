namespace Application.Exception;

public class AlreadyReviewedGameException : System.Exception
{
    public AlreadyReviewedGameException() : base("You already created a review for this game.")
    {
        
    }
}
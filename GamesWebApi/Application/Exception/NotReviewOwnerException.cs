namespace Application.Exception;

public class NotReviewOwnerException : System.Exception
{
    public NotReviewOwnerException() : base("You are not the owner of this review.")
    { }
}
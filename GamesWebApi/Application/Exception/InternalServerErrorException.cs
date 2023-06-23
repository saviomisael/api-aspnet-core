namespace Application.Exception;

public class InternalServerErrorException : System.Exception
{
    public InternalServerErrorException() : base("Internal Server Error.")
    {
    }
}
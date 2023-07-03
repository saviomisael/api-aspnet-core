namespace Application.Exception;

public class CreateAccountFailureException : System.Exception
{
    public string[] Errors { get; set; }

    public CreateAccountFailureException(string[] errors)
    {
        Errors = errors;
    }
}
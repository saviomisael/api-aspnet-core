namespace Application.Exception;

public class ChangePasswordFailureException : System.Exception
{
    public string[] Errors { get; private set; }
    public ChangePasswordFailureException(string[] errors)
    {
        Errors = errors;
    }
}
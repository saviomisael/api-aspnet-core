namespace Application.Exception;

public class LoginFailureException : System.Exception
{
    public LoginFailureException() : base("Login fails.")
    {
    }
}
namespace Application.Exception;

public class EmailInUseException : System.Exception
{
    public EmailInUseException(string email) : base($"Email {email} already is in use.")
    { }
}
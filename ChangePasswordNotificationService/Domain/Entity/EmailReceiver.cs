namespace Domain.Entity;

public class EmailReceiver
{
    public string UserName { get; private set; }
    public string Email { get; private set; }

    public EmailReceiver(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }
}
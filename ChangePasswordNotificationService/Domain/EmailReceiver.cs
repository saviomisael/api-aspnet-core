namespace Domain;

public class EmailReceiver
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public EmailReceiver(string name, string email)
    {
        Name = name;
        Email = email;
    }
}
namespace Domain.Entity;

public class Platform : AggregateRoot
{
    public string Name { get; }

    public Platform(string name)
    {
        Name = name;
    }
}
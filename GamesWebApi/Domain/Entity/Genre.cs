namespace Domain.Entity;

public class Genre : AggregateRoot
{
    public string Name { get; }

    public Genre(string name)
    {
        Name = name;
    }
}
namespace Domain.Entity;

public class Platform : AggregateRoot
{
    public string Name { get; }
    public ICollection<Game> Games { get; set; }

    public Platform(string name)
    {
        Name = name;
        Games = new List<Game>();
    }
}
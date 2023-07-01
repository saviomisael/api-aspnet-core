namespace Domain.Entity;

public class Platform : AggregateRoot
{
    public string Name { get; } = string.Empty;
    public ICollection<Game> Games { get; set; } = new List<Game>();

    public Platform(string name)
    {
        Name = name;
    }

    public Platform()
    {
        
    }
}
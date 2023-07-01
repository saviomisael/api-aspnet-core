namespace Domain.Entity;

public class Genre : AggregateRoot
{
    public string Name { get; } = string.Empty;
    public ICollection<Game> Games { get; set; } = new List<Game>();

    public Genre(string name)
    {
        Name = name;
        Games = new List<Game>();
    }

    public Genre()
    {
        
    }
}
namespace Domain.Entity;

public class Genre : AggregateRoot
{
    public string Name { get; }
    public ICollection<Game> Games { get; set; }

    public Genre(string name)
    {
        Name = name;
        Games = new List<Game>();
    }
}
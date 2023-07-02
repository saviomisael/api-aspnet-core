namespace Domain.Entity;

public class Genre : AggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Game>? Games { get; set; }

    public Genre(string name)
    {
        Name = name;
    }

    public Genre()
    {
        
    }
}
namespace Domain.Entity;

public class Platform : AggregateRoot
{
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Game>? Games { get; set; }

    public Platform(string name)
    {
        Name = name;
    }

    public Platform()
    {
        
    }
}
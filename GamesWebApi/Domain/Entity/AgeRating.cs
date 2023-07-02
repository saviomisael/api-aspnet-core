namespace Domain.Entity;

public class AgeRating : AggregateRoot
{
    public string Age { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual ICollection<Game>? Games { get; set; }

    public AgeRating(string age, string description)
    {
        Age = age;
        Description = description;
    }

    public AgeRating()
    {
        
    }
}
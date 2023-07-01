namespace Domain.Entity;

public class AgeRating : AggregateRoot
{
    public string Age { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public ICollection<Game> Games { get; set; } = new List<Game>();

    public AgeRating(string age, string description)
    {
        Age = age;
        Description = description;
        Games = new List<Game>();
    }

    public AgeRating()
    {
        
    }
}
namespace Domain.Entity;

public class AgeRating : AggregateRoot
{
    public string Age { get; }
    public string Description { get; }
    public ICollection<Game> Games { get; set; }

    public AgeRating(string age, string description)
    {
        Age = age;
        Description = description;
        Games = new List<Game>();
    }
}
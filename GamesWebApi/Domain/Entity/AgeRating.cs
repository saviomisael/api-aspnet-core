namespace Domain.Entity;

public class AgeRating : AggregateRoot
{
    public string Age { get; set; }
    public string Description { get; set; }
}
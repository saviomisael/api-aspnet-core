namespace Domain.Entity;

public class Platform : AggregateRoot
{
    public string Name { get; set; } = default!;
}
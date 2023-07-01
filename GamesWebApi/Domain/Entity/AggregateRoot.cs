namespace Domain.Entity;

public abstract class AggregateRoot
{
    public string Id { get; set; }

    protected AggregateRoot()
    {
        Id = Guid.NewGuid().ToString();
    }
}
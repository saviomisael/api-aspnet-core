namespace Domain.Entity;

public abstract class AggregateRoot
{
    public string Id { get; }

    protected AggregateRoot()
    {
        Id = Guid.NewGuid().ToString();
    }
}
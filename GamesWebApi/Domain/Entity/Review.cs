namespace Domain.Entity;

public class Review
{
    public string Id { get; set; }
    public string Description { get; set; }
    public int Stars { get; set; }
    public string GameId { get; set; }
    public virtual Game Game { get; set; }
    public string ReviewerId { get; set; }
    public virtual Reviewer Reviewer { get; set; }
}
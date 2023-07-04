namespace Domain.ValueObjects;

public class ReviewerInfo
{
    public DateTime CreatedAtUtcTime { get; set; }
    public int ReviewsCount { get; set; }
}
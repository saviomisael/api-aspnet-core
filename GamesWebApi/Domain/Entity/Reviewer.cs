namespace Domain.Entity;

public class Reviewer
{
    public string? TemporaryPassword { get; set; }
    public DateTime? TempPasswordTime { get; set; }
    public DateTime CreatedAt { get; set; }

    public Reviewer()
    {
        CreatedAt = DateTime.UtcNow;
        TempPasswordTime = null;
        TemporaryPassword = null;
    }
}
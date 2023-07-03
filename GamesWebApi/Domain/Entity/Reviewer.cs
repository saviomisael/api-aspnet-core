using Microsoft.AspNetCore.Identity;

namespace Domain.Entity;

public class Reviewer : IdentityUser
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
namespace Domain.DTO;

public class ReviewerTokenDto
{
    public string Token { get; set; }
    public string Username { get; set; }
    public DateTime ExpirationUtcTime { get; set; }
}
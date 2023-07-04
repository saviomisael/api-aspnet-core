namespace Domain.DTO;

public class ReviewResponseDto
{
    public string Id { get; set; }
    public string Description { get; set; }
    public int Stars { get; set; }
    public ReviewerResponseDto Reviewer { get; set; }
}
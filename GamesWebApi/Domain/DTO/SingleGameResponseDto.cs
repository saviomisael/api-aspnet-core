namespace Domain.DTO;

public class SingleGameResponseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UrlImage { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public AgeRatingResponseDto AgeRating { get; set; }
    public ICollection<GenreResponseDto> Genres { get; set; }
    public ICollection<PlatformResponseDto> Platforms { get; set; }
    public ICollection<ReviewResponseDto> Reviews { get; set; }
}
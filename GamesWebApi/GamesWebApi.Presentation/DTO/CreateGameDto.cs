namespace GamesWebApi.DTO;

public class CreateGameDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ReleaseDate { get; set; }
    public string AgeRatingId { get; set; }
    public ICollection<string> GenresNames { get; set; } = new List<string>();
    public ICollection<string> PlatformsNames { get; set; } = new List<string>();
    public IFormFile Image { get; set; } = null!;
}
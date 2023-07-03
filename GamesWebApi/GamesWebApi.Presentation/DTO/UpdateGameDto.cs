namespace GamesWebApi.DTO;

public class UpdateGameDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ReleaseDate { get; set; }
    public string AgeRatingId { get; set; }
    public ICollection<string> GenresNames { get; set; } = new List<string>();
    public ICollection<string> PlatformsNames { get; set; } = new List<string>();
}
namespace GamesWebApi.DTO;

public class CreateGameDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<string> Genres { get; set; } = new List<string>();
    public ICollection<string> Platforms { get; set; } = new List<string>();
    public IFormFile Image { get; set; } = null!;
}
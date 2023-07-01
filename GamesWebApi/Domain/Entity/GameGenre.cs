namespace Domain.Entity;

public class GameGenre
{
    public string GamesId { get; set; } = string.Empty;
    public string GenresId { get; set; } = string.Empty;
    public Game Game { get; set; } = null!;
    public Genre Genre { get; set; } = null!;
}
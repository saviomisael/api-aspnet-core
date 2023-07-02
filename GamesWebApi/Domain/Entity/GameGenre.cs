namespace Domain.Entity;

public class GameGenre
{
    public string GamesId { get; set; } = string.Empty;
    public string GenresId { get; set; } = string.Empty;
    public virtual Game Game { get; set; } = null!;
    public virtual Genre Genre { get; set; } = null!;
}
namespace Domain.Entity;

public class GamePlatform
{
    public string GamesId { get; set; } = string.Empty;
    public string PlatformsId { get; set; } = string.Empty;
    public virtual Game Game { get; set; } = null!;
    public virtual Platform Platform { get; set; } = null!;
}
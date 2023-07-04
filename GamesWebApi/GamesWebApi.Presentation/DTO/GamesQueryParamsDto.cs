namespace GamesWebApi.DTO;

public class GamesQueryParamsDto
{
    public int? Page { get; set; }
    public string? Term { get; set; }
    public string? Sort { get; set; }
}
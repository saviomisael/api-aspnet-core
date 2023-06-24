namespace GamesWebApi.DTO;

public class ErrorResponseDto
{
    public IList<string> Errors { get; set; } = default!;
}
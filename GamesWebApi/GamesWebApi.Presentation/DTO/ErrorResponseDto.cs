namespace GamesWebApi.DTO;

public class ErrorResponseDto
{
    public IList<string> Errors { get; set; }

    public ErrorResponseDto()
    {
        Errors = new List<string>();
    }
}
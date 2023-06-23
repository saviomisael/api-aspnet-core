namespace GamesWebApi.DTO;

public class ErrorResponseDTO
{
    public IList<string> Errors { get; }

    public ErrorResponseDTO()
    {
        Errors = new List<string>();
    }
}
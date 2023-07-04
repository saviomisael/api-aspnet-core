using Domain.DTO;

namespace GamesWebApi.DTO;

public class GetAllGamesResponseDto
{
    public ICollection<GameResponseDto> Games { get; set; }
    public int CurrentPage { get; set; }
    public int? NextPage { get; set; }
    public int? PreviousPage { get; set; }
    public int LastPage { get; set; }
}
using Domain.Entity;
using GamesWebApi.DTO;

namespace GamesWebApi.Mapper;

public static class GameMapper
{
    public static Game FromCreateGameDtoToEntity(CreateGameDto dto, string imageUrl)
    {
        var game = new Game
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ReleaseDate = DateTime.Parse(dto.ReleaseDate),
            AgeRating = new AgeRating()
            {
                Id = dto.AgeRatingId
            },
            UrlImage = imageUrl
        };

        foreach (var genre in dto.Genres)
        {
            game.AddGenre(new Genre { Id = genre });
        }

        foreach (var platform in dto.Platforms)
        {
            game.AddPlatform(new Platform { Id = platform });
        }

        return game;
    }
}
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

        foreach (var name in dto.GenresNames)
        {
            game.AddGenre(new Genre { Name = name});
        }

        foreach (var name in dto.PlatformsNames)
        {
            game.AddPlatform(new Platform { Name = name });
        }

        return game;
    }
}
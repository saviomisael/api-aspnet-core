using GamesWebApi.DTO;
using GamesWebApi.Migrations;

namespace GamesWebApi.IoC;

public static class MapperExtensions
{
    public static void AddMapper(this IServiceCollection service)
    {
        service.AddAutoMapper((cfg) =>
        {
            cfg.CreateMap<CreateGenreDto, Genre>();
        });
    }
}
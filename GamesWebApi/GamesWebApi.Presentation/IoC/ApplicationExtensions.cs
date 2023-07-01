using Application.Service;
using Domain.Service;

namespace GamesWebApi.IoC;

public static class ApplicationExtensions
{
    public static void AddAppDependencies(this IServiceCollection service)
    {
        service.AddScoped<IGenreService, GenreService>();
        service.AddScoped<IPlatformService, PlatformService>();
        service.AddScoped<IAgeRatingService, AgeRatingService>();
        service.AddScoped<IGameService, GameService>();
    }
}
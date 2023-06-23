using Application.Service;
using Application.Service.Contracts;

namespace GamesWebApi.IoC;

public static class ApplicationExtensions
{
    public static void AddAppDependencies(this IServiceCollection service)
    {
        service.AddScoped<IGenreService, GenreService>();
    }
}
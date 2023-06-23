using Application.Service;
using Application.Service.Contracts;

namespace GamesWebApi.IoC;

public static class ApplicationExtensions
{
    public static void addAppDependencies(this IServiceCollection service)
    {
        service.AddScoped<IGenreService, GenreService>();
    }
}
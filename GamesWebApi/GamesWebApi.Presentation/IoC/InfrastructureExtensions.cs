using Domain.Repository;
using Infrastructure.Data.Repository;

namespace GamesWebApi.IoC;

public static class InfrastructureExtensions
{
    public static void addInfraDependecies(this IServiceCollection service)
    {
        service.AddScoped<IGenreRepository, GenreRepository>();
    }
}
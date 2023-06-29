using Domain.Repository;
using Infrastructure.Data;
using Infrastructure.Data.Repository;

namespace GamesWebApi.IoC;

public static class InfrastructureExtensions
{
    public static void AddInfraDependencies(this IServiceCollection service)
    {
        service.AddScoped<IGenreRepository, GenreRepository>();
        service.AddScoped<IPlatformRepository, PlatformRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
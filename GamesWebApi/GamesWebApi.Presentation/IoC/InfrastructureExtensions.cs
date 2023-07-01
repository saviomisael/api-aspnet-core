using Domain.Repository;
using Infrastructure.Data;
using Infrastructure.Data.Repository;
using Infrastructure.ImagesServerApi;
using Infrastructure.ImagesServerApi.Contracts;

namespace GamesWebApi.IoC;

public static class InfrastructureExtensions
{
    public static void AddInfraDependencies(this IServiceCollection service)
    {
        service.AddScoped<IGenreRepository, GenreRepository>();
        service.AddScoped<IPlatformRepository, PlatformRepository>();
        service.AddScoped<IAgeRatingRepository, AgeRatingRepository>();
        service.AddScoped<IGameRepository, GameRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddTransient<DataSeeder>();
    }
}
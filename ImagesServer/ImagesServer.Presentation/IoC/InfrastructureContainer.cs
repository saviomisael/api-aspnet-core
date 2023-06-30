using Domain.Repository;
using Infrastructure.Data;
using Infrastructure.Data.Repository;

namespace ImagesServer.IoC;

public static class InfrastructureContainer
{
    public static void AddInfraDependencies(this IServiceCollection collection)
    {
        collection.AddScoped<IImageRepository, ImageRepository>();
        collection.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
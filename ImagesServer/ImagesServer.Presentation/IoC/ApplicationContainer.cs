using Application.Service;
using Domain.Service;

namespace ImagesServer.IoC;

public static class ApplicationContainer
{
    public static void AddApplicationDependencies(this IServiceCollection collection)
    {
        collection.AddScoped<IImageService, ImageService>();
    }
}
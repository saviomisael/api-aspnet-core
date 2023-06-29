using Domain.Entity;

namespace Application.Service.Contracts;

public interface IPlatformService
{
    Task<Platform> CreatePlatform(Platform platform);
}
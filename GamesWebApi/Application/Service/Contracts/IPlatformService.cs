using Domain.Entity;

namespace Application.Service.Contracts;

public interface IPlatformService
{
    Task<Platform> CreatePlatformAsync(Platform platform);
    Task<ICollection<Platform>> GetAllAsync();
}
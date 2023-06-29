using Domain.Entity;

namespace Domain.Service;

public interface IPlatformService
{
    Task<Platform> CreatePlatformAsync(Platform platform);
    Task<ICollection<Platform>> GetAllAsync();
    Task DeleteByNameAsync(string name);
}
using Domain.Entity;

namespace Domain.Repository;

public interface IPlatformRepository
{
    Task<Platform?> GetByNameAsync(string name);
    void CreatePlatform(Platform platform);
    Task<ICollection<Platform>> GetAllAsync();
    void DeleteByName(Platform platform);
    Task<bool> PlatformExistsAsync(string platformId);
}
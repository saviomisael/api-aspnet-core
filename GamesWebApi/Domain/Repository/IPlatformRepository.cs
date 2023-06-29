using Domain.Entity;

namespace Domain.Repository;

public interface IPlatformRepository
{
    Task<Platform?> GetByNameAsync(string name);
    void CreatePlatform(Platform platform);
}
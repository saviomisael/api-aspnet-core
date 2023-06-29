using Domain.Entity;

namespace Domain.Repository;

public interface IPlatformRepository
{
    Task<Platform?> GetByName(string name);
    void CreatePlatform(Platform platform);
}
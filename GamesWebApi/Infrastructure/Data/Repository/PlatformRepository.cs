using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _context;

    public PlatformRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Platform?> GetByNameAsync(string name)
    {
        return await _context.Platforms.FirstOrDefaultAsync(x => x.Name == name);
    }

    public void CreatePlatform(Platform platform)
    {
        _context.Platforms.Add(platform);
    }
}
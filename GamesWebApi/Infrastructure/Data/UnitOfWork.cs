using Domain.Repository;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenreRepository GenreRepository { get; }
    public IPlatformRepository PlatformRepository { get; }
    
    public UnitOfWork(AppDbContext context, IGenreRepository genreRepository, IPlatformRepository platformRepository)
    {
        _context = context;
        GenreRepository = genreRepository;
        PlatformRepository = platformRepository;
    }
    
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
using Domain.Repository;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenreRepository GenreRepository { get; }
    
    public UnitOfWork(AppDbContext context, IGenreRepository genreRepository)
    {
        _context = context;
        GenreRepository = genreRepository;
    }
    
    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
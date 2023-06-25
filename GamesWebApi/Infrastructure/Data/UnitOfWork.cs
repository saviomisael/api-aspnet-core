using Domain.Repository;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public readonly IGenreRepository GenreRepository;
    
    public UnitOfWork(AppDbContext context, IGenreRepository genreRepository)
    {
        _context = context;
        GenreRepository = genreRepository;
    }
    
    public async void Commit()
    {
        await _context.SaveChangesAsync();
    }
}
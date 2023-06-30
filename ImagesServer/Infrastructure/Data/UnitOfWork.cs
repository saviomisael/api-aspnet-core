using Domain.Repository;
using Infrastructure.Data;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    public IImageRepository ImageRepository { get; }
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context, IImageRepository repository)
    {
        ImageRepository = repository;
        _context = context;
    }
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
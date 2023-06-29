using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository;

public class AgeRatingRepository : IAgeRatingRepository
{
    private readonly AppDbContext _context;

    public AgeRatingRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<AgeRating>> GetAllAsync()
    {
        return await _context.AgeRatings.ToListAsync();
    }
}
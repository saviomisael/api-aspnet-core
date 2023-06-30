using Domain.Entity;
using Domain.Repository;

namespace Infrastructure.Data.Repository;

public class ImageRepository : IImageRepository
{
    private readonly AppDbContext _context;

    public ImageRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void SaveImage(Image image)
    {
        _context.Images.Add(image);
    }
}
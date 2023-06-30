using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> ImageAlreadyExists(string imageName)
    {
        var image = await _context.Images.FirstOrDefaultAsync(x => x.Name == imageName);

        return image != null;
    }
}
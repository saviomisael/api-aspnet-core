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

    public async Task<bool> ImageAlreadyExistsAsync(string imageName)
    {
        var image = await _context.Images.FirstOrDefaultAsync(x => x.Name == imageName);

        return image != null;
    }

    public async Task<Image?> GetImageAsync(string name)
    {
        return await _context.Images.FirstOrDefaultAsync(x => x.Name == name);
    }

    public void DeleteImage(Image image)
    {
        _context.Images.Remove(image);
    }
}
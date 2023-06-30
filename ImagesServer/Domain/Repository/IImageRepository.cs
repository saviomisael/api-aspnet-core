using Domain.Entity;

namespace Domain.Repository;

public interface IImageRepository
{
    void SaveImage(Image image);
    Task<bool> ImageAlreadyExistsAsync(string imageName);
    Task<Image?> GetImageAsync(string name);
}
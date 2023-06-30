using Domain.Entity;

namespace Domain.Repository;

public interface IImageRepository
{
    void SaveImage(Image image);
    Task<bool> ImageAlreadyExists(string imageName);
    Task<Image?> GetImage(string name);
}
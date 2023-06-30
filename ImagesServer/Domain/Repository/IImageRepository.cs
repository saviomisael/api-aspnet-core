using Domain.Entity;

namespace Domain.Repository;

public interface IImageRepository
{
    void SaveImage(Image image);
}
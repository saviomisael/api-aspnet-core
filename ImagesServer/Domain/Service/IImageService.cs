using Domain.DTO;
using Domain.Entity;

namespace Domain.Service;

public interface IImageService
{
    Task<ImageResponseDto> CreateImageAsync(Image image);
}
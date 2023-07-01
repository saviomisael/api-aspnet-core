using Domain.DTO;

namespace Infrastructure.ImagesServerApi.Contracts;

public interface IImagesServerApiClient
{
    Task<ImageResponseDto?> PostImageAsync(byte[] imageBytes, string contentType, string imageName);
}
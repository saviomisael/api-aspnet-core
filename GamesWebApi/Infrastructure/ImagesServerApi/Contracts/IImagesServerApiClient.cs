using Domain.DTO;

namespace Infrastructure.ImagesServerApi.Contracts;

public interface IImagesServerApiClient
{
    Task<ImageResponseDto?> PostImageAsync(Stream imageStream, string contentType, string imageName);
}
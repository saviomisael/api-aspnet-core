using Domain.DTO;

namespace Infrastructure.ImagesServerApi.Contracts;

public interface IImagesServerApiClient
{
    Task<ImageResponseDto?> PostImage(Stream imageStream, string contentType, string imageName);
}
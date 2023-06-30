using Application.Options;
using Domain.DTO;
using Domain.Entity;
using Domain.Service;
using Infrastructure.Data;

namespace Application.Service;

public class ImageService : IImageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DomainOptions _domainOptions;

    public ImageService(IUnitOfWork unitOfWork, DomainOptions options)
    {
        _unitOfWork = unitOfWork;
        _domainOptions = options;
    }
    
    public async Task<ImageResponseDto> CreateImageAsync(Image image)
    {
        var nameAlreadyExists = await _unitOfWork.ImageRepository.ImageAlreadyExistsAsync(image.Name);

        while (nameAlreadyExists)
        {
            image.GenerateNewName();
            nameAlreadyExists = await _unitOfWork.ImageRepository.ImageAlreadyExistsAsync(image.Name);
        }
        
        _unitOfWork.ImageRepository.SaveImage(image);
        await _unitOfWork.CommitAsync();

        return new ImageResponseDto(image.Name, _domainOptions.Domain + "/api/v1/images/" + image.Name);
    }
}
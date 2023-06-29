using Application.Exception;
using Domain.Entity;
using Domain.Service;
using Infrastructure.Data;

namespace Application.Service;

public class PlatformService : IPlatformService
{
    private readonly IUnitOfWork _unitOfWork;

    public PlatformService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Platform> CreatePlatformAsync(Platform platform)
    {
        var platformAlreadyExists = await _unitOfWork.PlatformRepository.GetByNameAsync(platform.Name);

        if (platformAlreadyExists != null)
        {
            throw new PlatformAlreadyExistsException(platform.Name);
        }
        
        _unitOfWork.PlatformRepository.CreatePlatform(platform);
        await _unitOfWork.Commit();

        var platformFromDb = await _unitOfWork.PlatformRepository.GetByNameAsync(platform.Name);

        if (platformFromDb is null)
        {
            throw new InternalServerErrorException();
        }

        return platformFromDb;
    }

    public async Task<ICollection<Platform>> GetAllAsync()
    {
        return await _unitOfWork.PlatformRepository.GetAllAsync();
    }
}
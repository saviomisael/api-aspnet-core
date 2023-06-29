using Application.Exception;
using Application.Service.Contracts;
using Domain.Entity;
using Infrastructure.Data;

namespace Application.Service;

public class PlatformService : IPlatformService
{
    private IUnitOfWork _unitOfWork;

    public PlatformService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Platform> CreatePlatform(Platform platform)
    {
        var platformAlreadyExists = _unitOfWork.PlatformRepository.GetByName(platform.Name);

        if (platformAlreadyExists != null)
        {
            throw new PlatformAlreadyExistsException(platform.Name);
        }
        
        _unitOfWork.PlatformRepository.CreatePlatform(platform);
        await _unitOfWork.Commit();

        var platformFromDb = await _unitOfWork.PlatformRepository.GetByName(platform.Name);

        if (platformFromDb is null)
        {
            throw new InternalServerErrorException();
        }

        return platform;
    }
}
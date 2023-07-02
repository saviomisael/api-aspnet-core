using Domain.DTO;
using Domain.Entity;

namespace GamesWebApi.Mapper;

public static class PlatformMapper
{
    public static PlatformResponseDto FromEntityToPlatformResponseDto(Platform platform)
    {
        return new PlatformResponseDto
        {
            Id = platform.Id,
            Name = platform.Name
        };
    }
}
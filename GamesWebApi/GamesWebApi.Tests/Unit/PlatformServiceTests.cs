using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Exception;
using Application.Service;
using Domain.Entity;
using Domain.Repository;
using FluentAssertions;
using Infrastructure.Data;
using Moq;
using Xunit;

namespace GamesWebApi.Tests.Unit;

public class PlatformServiceTests
{
    private readonly Mock<IPlatformRepository> _platformRepoMock;
    private readonly IUnitOfWork _unitOfWork;

    public PlatformServiceTests()
    {
        _platformRepoMock = new Mock<IPlatformRepository>();
        var context = new AppDbContext(AppDbContextOptions.GetInMemoryOptions());
        _unitOfWork = new UnitOfWork(context);
    }

    [Fact]
    public async void CreatePlatform_ShouldThrowsPlatformAlreadyExistsException_WhenPlatformAlreadyExists()
    {
        _platformRepoMock.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(new Platform("platform"));

        _unitOfWork.PlatformRepository = _platformRepoMock.Object;
        
        var service = new PlatformService(_unitOfWork);

        await service.Invoking(x => x.CreatePlatformAsync(new Platform("platform"))).Should()
            .ThrowAsync<PlatformAlreadyExistsException>().WithMessage("Platform platform already exists.");
    }

    [Fact]
    public async void CreatePlatform_ShouldReturnNewlyPlatform_WhenPlatformDoesNotAlreadyExist()
    {
        _platformRepoMock.SetupSequence(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Platform?)null)
            .ReturnsAsync(new Platform("xbox"));

        _unitOfWork.PlatformRepository = _platformRepoMock.Object;
        
        var service = new PlatformService(_unitOfWork);

        var result = await service.CreatePlatformAsync(new Platform("xbox"));

        result.Should().BeOfType<Platform>();
        result.Name.Should().Be("xbox");
    }

    [Fact]
    public async void DeleteByNameAsync_ShouldDeleteAPlatform()
    {
        _platformRepoMock.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(new Platform("platform") { Games = new List<Game>()});
        _platformRepoMock.Setup(x => x.DeleteByName(It.IsAny<Platform>())).Verifiable();
        
        _unitOfWork.PlatformRepository = _platformRepoMock.Object;
        
        var service = new PlatformService(_unitOfWork);
        
        await service.DeleteByNameAsync("platform");
    }

    [Fact]
    public async void DeleteByNameAsync_ShouldThrowPlatformNotFoundException()
    {
        _platformRepoMock.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Platform?)null);

        _unitOfWork.PlatformRepository = _platformRepoMock.Object;
        
        var service = new PlatformService(_unitOfWork);
        
        await service.Invoking(x => x.DeleteByNameAsync(It.IsAny<string>())).Should().ThrowAsync<PlatformNotFoundException>();
    }
}
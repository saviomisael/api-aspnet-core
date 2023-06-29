using System;
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
    private readonly Mock<IGenreRepository> _genreRepoMock;
    private readonly AppDbContext _context;
    private readonly Mock<IPlatformRepository> _platformRepoMock;

    public PlatformServiceTests()
    {
        _genreRepoMock = new Mock<IGenreRepository>();
        _context = new AppDbContext(AppDbContextOptions.GetInMemoryOptions());
        _platformRepoMock = new Mock<IPlatformRepository>();
    }

    [Fact]
    public async void CreatePlatform_ShouldThrowsPlatformAlreadyExistsException_WhenPlatformAlreadyExists()
    {
        _platformRepoMock.Setup(x => x.GetByName(It.IsAny<string>())).ReturnsAsync(new Platform("platform"));

        var service = new PlatformService(new UnitOfWork(_context, _genreRepoMock.Object, _platformRepoMock.Object));

        await service.Invoking(x => x.CreatePlatform(new Platform("platform"))).Should()
            .ThrowAsync<PlatformAlreadyExistsException>().WithMessage("Platform platform already exists.");
    }

    [Fact]
    public async void CreatePlatform_ShouldReturnNewlyPlatform_WhenPlatformDoesNotAlreadyExist()
    {
        _platformRepoMock.SetupSequence(repo => repo.GetByName(It.IsAny<string>())).ReturnsAsync((Platform?)null)
            .ReturnsAsync(new Platform("xbox"));

        var service = new PlatformService(new UnitOfWork(_context, _genreRepoMock.Object, _platformRepoMock.Object));

        var result = await service.CreatePlatform(new Platform("xbox"));

        result.Should().BeOfType<Platform>();
        result.Name.Should().Be("xbox");
    }
}
using System.Collections.Generic;
using Application.Exception;
using Application.Service;
using Domain.Entity;
using Domain.Repository;
using FluentAssertions;
using Infrastructure.Data;
using Moq;
using Xunit;

namespace GamesWebApi.Tests.Unit;

public class GameServiceTests
{
    private readonly Mock<IGameRepository> _gameRepoMock;
    private readonly Mock<IAgeRatingRepository> _ageRepoMock;
    private readonly Mock<IGenreRepository> _genreRepoMock;
    private readonly Mock<IPlatformRepository> _platformRepoMock;
    private readonly IUnitOfWork _unitOfWork;

    public GameServiceTests()
    {
        _gameRepoMock = new Mock<IGameRepository>();
        _ageRepoMock = new Mock<IAgeRatingRepository>();
        _genreRepoMock = new Mock<IGenreRepository>();
        _platformRepoMock = new Mock<IPlatformRepository>();
        var context = new AppDbContext(AppDbContextOptions.GetInMemoryOptions());
        _unitOfWork = new UnitOfWork(context);
    }

    [Fact]
    public async void CreateGame_ShouldThrowAgeNotFoundException_WhenAgeDoesNotExist()
    {
        _ageRepoMock.Setup(x => x.AgeExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

        _unitOfWork.AgeRatingRepository = _ageRepoMock.Object;

        var service = new GameService(_unitOfWork);

        var game = new Game
        {
            AgeRating = new AgeRating("3+", "description")
        };

        await service.Invoking(x => x.CreateGame(game)).Should().ThrowAsync<AgeNotFoundException>();
    }

    [Fact]
    public async void CreateGame_ShouldThrowGenreNotFoundException_WhenGenreDoesNotExist()
    {
        _ageRepoMock.Setup(x => x.AgeExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _genreRepoMock.Setup(x => x.GenreExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

        _unitOfWork.AgeRatingRepository = _ageRepoMock.Object;
        _unitOfWork.GenreRepository = _genreRepoMock.Object;

        var service = new GameService(_unitOfWork);

        var game = new Game
        {
            AgeRating = new AgeRating("3+", "description"),
            Genres = new List<Genre>() { new("genre") }
        };

        await service.Invoking(x => x.CreateGame(game)).Should().ThrowAsync<GenreNotFoundException>();
    }

    [Fact]
    public async void CreateGame_ShouldThrowPlatformNotFoundException_WhenPlatformDoesNotExist()
    {
        _ageRepoMock.Setup(x => x.AgeExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _genreRepoMock.Setup(x => x.GenreExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _platformRepoMock.Setup(x => x.PlatformExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

        _unitOfWork.AgeRatingRepository = _ageRepoMock.Object;
        _unitOfWork.GenreRepository = _genreRepoMock.Object;
        _unitOfWork.PlatformRepository = _platformRepoMock.Object;

        var service = new GameService(_unitOfWork);

        var game = new Game
        {
            AgeRating = new AgeRating("3+", "description"),
            Genres = new List<Genre>() { new("genre") },
            Platforms = new List<Platform>() { new("platform") }
        };
        
        await service.Invoking(x => x.CreateGame(game)).Should().ThrowAsync<PlatformNotFoundException>();
    }
}
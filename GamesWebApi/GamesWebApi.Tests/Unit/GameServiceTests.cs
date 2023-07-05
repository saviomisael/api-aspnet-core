using System.Collections.Generic;
using Application.Exception;
using Application.Service;
using Domain.Entity;
using Domain.Repository;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.ImagesServerApi.Contracts;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace GamesWebApi.Tests.Unit;

public class GameServiceTests
{
    private readonly Mock<IAgeRatingRepository> _ageRepoMock;
    private readonly Mock<IGenreRepository> _genreRepoMock;
    private readonly Mock<IPlatformRepository> _platformRepoMock;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Mock<IImagesServerApiClient> _apiClientMock;
    private readonly Mock<UserManager<Reviewer>> _userManagerMock;
    private readonly Mock<IGameRepository> _gameRepoMock;

    public GameServiceTests()
    {
        _ageRepoMock = new Mock<IAgeRatingRepository>();
        _genreRepoMock = new Mock<IGenreRepository>();
        _platformRepoMock = new Mock<IPlatformRepository>();
        _gameRepoMock = new Mock<IGameRepository>();
        var context = new AppDbContext(AppDbContextOptions.GetInMemoryOptions());
        _unitOfWork = new UnitOfWork(context);
        _apiClientMock = new Mock<IImagesServerApiClient>();
        _userManagerMock = new Mock<UserManager<Reviewer>>(Mock.Of<IUserStore<Reviewer>>(), null, null, null, null, null, null, null, null);
    }

    [Fact]
    public async void CreateGame_ShouldThrowAgeNotFoundException_WhenAgeDoesNotExist()
    {
        _gameRepoMock.Setup(x => x.IsGameNameInUseAsync(It.IsAny<string>())).ReturnsAsync(false);
        _ageRepoMock.Setup(x => x.AgeExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

        _unitOfWork.AgeRatingRepository = _ageRepoMock.Object;
        _unitOfWork.GameRepository = _gameRepoMock.Object;

        var service = new GameService(_unitOfWork, _apiClientMock.Object, _userManagerMock.Object);

        var game = new Game
        {
            AgeRating = new AgeRating("3+", "description")
        };

        await service.Invoking(x => x.CreateGameAsync(game)).Should().ThrowAsync<AgeNotFoundException>();
    }

    [Fact]
    public async void CreateGame_ShouldThrowGenreNotFoundException_WhenGenreDoesNotExist()
    {
        _gameRepoMock.Setup(x => x.IsGameNameInUseAsync(It.IsAny<string>())).ReturnsAsync(false);
        _ageRepoMock.Setup(x => x.AgeExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _genreRepoMock.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Genre?)null);

        _unitOfWork.AgeRatingRepository = _ageRepoMock.Object;
        _unitOfWork.GenreRepository = _genreRepoMock.Object;
        _unitOfWork.GameRepository = _gameRepoMock.Object;

        var service = new GameService(_unitOfWork, _apiClientMock.Object, _userManagerMock.Object);

        var game = new Game
        {
            AgeRating = new AgeRating("3+", "description"),
            Genres = new List<Genre>() { new("genre") }
        };

        await service.Invoking(x => x.CreateGameAsync(game)).Should().ThrowAsync<GenreNotFoundException>();
    }

    [Fact]
    public async void CreateGame_ShouldThrowPlatformNotFoundException_WhenPlatformDoesNotExist()
    {
        _gameRepoMock.Setup(x => x.IsGameNameInUseAsync(It.IsAny<string>())).ReturnsAsync(false);
        _ageRepoMock.Setup(x => x.AgeExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
        _genreRepoMock.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync(new Genre("genre"));
        _platformRepoMock.Setup(x => x.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Platform?)null);

        _unitOfWork.AgeRatingRepository = _ageRepoMock.Object;
        _unitOfWork.GenreRepository = _genreRepoMock.Object;
        _unitOfWork.PlatformRepository = _platformRepoMock.Object;
        _unitOfWork.GameRepository = _gameRepoMock.Object;

        var service = new GameService(_unitOfWork, _apiClientMock.Object, _userManagerMock.Object);

        var game = new Game
        {
            AgeRating = new AgeRating("3+", "description"),
            Genres = new List<Genre>() { new("genre") },
            Platforms = new List<Platform>() { new("platform") }
        };
        
        await service.Invoking(x => x.CreateGameAsync(game)).Should().ThrowAsync<PlatformNotFoundException>();
    }
}
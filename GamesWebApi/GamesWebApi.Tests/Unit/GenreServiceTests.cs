using System.Linq;
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

public class GenreServiceTests
{
    private readonly Mock<IGenreRepository> _repoMock;
    private readonly AppDbContext _context;
    private readonly Mock<IPlatformRepository> _platformRepoMock;

    public GenreServiceTests()
    {
        _repoMock = new Mock<IGenreRepository>();
        _context = new AppDbContext(AppDbContextOptions.GetInMemoryOptions());
        _platformRepoMock = new Mock<IPlatformRepository>();
    }
    [Fact]
    public async Task CreateGenre_ShouldThrowGenreAlreadyExistsException()
    {
        _repoMock.Setup(repository => repository.GetByNameAsync(It.IsAny<string>())).Throws(new GenreAlreadyExistsException("test"));
        
        var service = new GenreService(new UnitOfWork(_context, _repoMock.Object, _platformRepoMock.Object));

        await service.Invoking(x => x.CreateGenreAsync(new Genre("test")))
            .Should().ThrowAsync<GenreAlreadyExistsException>()
            .WithMessage("Genre test already exists.");
    }

    [Fact]
    public async Task CreateGenre_ShouldCreateAGenre()
    {
        _repoMock.SetupSequence(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Genre?)null).ReturnsAsync(new Genre("action"));

        var service = new GenreService(new UnitOfWork(_context, _repoMock.Object, _platformRepoMock.Object));
        
        var result = await service.CreateGenreAsync(new Genre("action"));

        result.Should().BeOfType<Genre>();
        result.Name.Should().BeEquivalentTo("action");
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllGenres()
    {
        _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new[]
        {
            new Genre("genre 1"),
            new Genre("genre 2"),
            new Genre("genre 3"),
            new Genre("genre 4")
        }.ToList());
        
        var service = new GenreService(new UnitOfWork(_context, _repoMock.Object, _platformRepoMock.Object));

        var result = await service.GetAllAsync();

        result.Count.Should().Be(4);
        result.FirstOrDefault(x => x.Name == "genre 1").Should().NotBeNull();
        result.FirstOrDefault(x => x.Name == "genre 2").Should().NotBeNull();
        result.FirstOrDefault(x => x.Name == "genre 3").Should().NotBeNull();
        result.FirstOrDefault(x => x.Name == "genre 4").Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteByName_ShouldThrowGenreNotFoundException_WhenGenreNotExists()
    {
        _repoMock.Setup(repo => repo.GetByNameAsync(It.IsAny<string>())).ReturnsAsync((Genre?)null);
        
        var service = new GenreService(new UnitOfWork(_context, _repoMock.Object, _platformRepoMock.Object));

        await service.Invoking(s => s.DeleteByNameAsync("genre")).Should().ThrowAsync<GenreNotFoundException>().WithMessage("Genre genre not found.");
    }
}
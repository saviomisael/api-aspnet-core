using System;
using System.Threading.Tasks;
using Application.Exception;
using Application.Service;
using Domain.Entity;
using Domain.Repository;
using FluentAssertions;
using Moq;
using Xunit;

namespace GamesWebApi.Tests.Unit;

public class GenreServiceTests
{
    private readonly Mock<IGenreRepository> _repoMock;

    public GenreServiceTests()
    {
        _repoMock = new Mock<IGenreRepository>();
    }
    [Fact]
    public async Task CreateGenre_ShouldThrowGenreAlreadyExistsException()
    {
        _repoMock.Setup(repository => repository.GetByName(It.IsAny<string>())).Throws(new GenreAlreadyExistsException("test"));
        
        var service = new GenreService(_repoMock.Object);

        await service.Invoking(x => x.CreateGenre(new Genre("test")))
            .Should().ThrowAsync<GenreAlreadyExistsException>()
            .WithMessage("Genre test already exists.");
    }

    [Fact]
    public async Task CreateGenre_ShouldCreateAGenre()
    {
        _repoMock.SetupSequence(repo => repo.GetByName(It.IsAny<string>())).ReturnsAsync((Genre?)null).ReturnsAsync(new Genre("action"));

        var service = new GenreService(_repoMock.Object);
        
        var result = await service.CreateGenre(new Genre("action"));

        result.Should().BeOfType<Genre>();
        result.Name.Should().BeEquivalentTo("action");
    }
}
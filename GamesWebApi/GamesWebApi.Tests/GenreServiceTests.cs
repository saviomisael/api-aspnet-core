using System;
using System.Threading.Tasks;
using Application.Exception;
using Application.Service;
using Domain.Entity;
using Domain.Repository;
using Moq;
using Xunit;

namespace GamesWebApi.Tests;

public class GenreServiceTests
{
    [Fact]
    public async Task CreateGenre_ShouldThrowGenreAlreadyExistsException()
    {
        var mock = new Mock<IGenreRepository>();

        mock.Setup(lib => lib.GetByName(It.IsAny<string>())).Throws(new GenreAlreadyExistsException("test"));
        
        var service = new GenreService(mock.Object);

        await Assert.ThrowsAsync<GenreAlreadyExistsException>( () => service.CreateGenre(new Genre("test")));
    }
}
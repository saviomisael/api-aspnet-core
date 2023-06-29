using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using FluentAssertions;
using GamesWebApi.DTO;
using GamesWebApi.V1;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace GamesWebApi.Tests.Functional;

public class GenreControllerTests : IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private AppDbContext _context = null!;

    public GenreControllerTests()
    {
        _factory = new WebApplicationFactory<Program>();
        InitContext();
    }

    [Fact]
    public async void CreateGenre_ShouldReturnBadRequest_WhenRequestIsInvalid()
    {
        var client = _factory.CreateClient();

        var request = new CreateGenreDto
        {
            Name = "a"
        };

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(ApiRoutes.GenreRoutes.Create, content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void CreateGenre_ShouldReturnCreatedWhenGenreIsValid()
    {
        var client = _factory.CreateClient();

        var request = new CreateGenreDto()
        {
            Name = "genre"
        };

        var body = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(ApiRoutes.GenreRoutes.Create, body);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async void GetAll_ShouldReturnAllGenresFromDB()
    {
        _context.Genres.Add(new Genre("genre 1"));
        _context.Genres.Add(new Genre("genre 2"));
        _context.Genres.Add(new Genre("genre 3"));
        _context.Genres.Add(new Genre("genre 4"));
        await _context.SaveChangesAsync();
        
        var client = _factory.CreateClient();

        var response = await client.GetAsync(ApiRoutes.GenreRoutes.GetAll);
        var body = response.Content.ReadAsStringAsync().Result;
        var genresFromBody = JsonConvert.DeserializeObject<ICollection<Genre>>(body);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        genresFromBody.Should().NotBeNull();
        genresFromBody.Count.Should().Be(4);
    }

    [Fact]
    public async void DeleteByName_ShouldReturnNotFound_WhenGenreDoesNotExist()
    {
        var client = _factory.CreateClient();

        var result = await client.DeleteAsync(ApiRoutes.GenreRoutes.DeleteByName.Replace("{name}", "genre"));
        var errors = ConvertResponseHelper.ToObject<ErrorResponseDto>(result);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        errors.Should().NotBeNull();
        errors.Errors.Contains("Genre genre not found.").Should().BeTrue();
    }

    [Fact]
    public async void DeleteByName_ShouldReturnNoContent_WhenGenreIsDeleted()
    {
        _context.Genres.Add(new Genre("genre"));
        await _context.SaveChangesAsync();
        
        var client = _factory.CreateClient();

        var result = await client.DeleteAsync(ApiRoutes.GenreRoutes.DeleteByName.Replace("{name}", "genre"));

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    private void InitContext()
    {
        _context = new AppDbContext(AppDbContextOptions.GetSqlServerOptions());
    }

    private async Task ClearData()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Genres");
    }

    public async Task InitializeAsync()
    {
        await ClearData();
    }

    public async Task DisposeAsync()
    {
        await ClearData();
    }
}
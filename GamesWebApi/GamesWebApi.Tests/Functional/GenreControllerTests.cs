using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GamesWebApi.DTO;
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

    private void InitContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(
                "Data Source=api_db; Initial Catalog=gamesdb; User Id=SA; Password=123aBc@#;TrustServerCertificate=True;Encrypt=False;")
            .Options;
        _context = new AppDbContext(options);
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
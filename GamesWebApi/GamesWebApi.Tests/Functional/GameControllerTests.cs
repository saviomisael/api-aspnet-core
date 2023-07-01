using System.Net;
using FluentAssertions;
using GamesWebApi.DTO;
using GamesWebApi.V1;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GamesWebApi.Tests.Functional;

public class GameControllerTests
{
    private AppDbContext _context;
    private WebApplicationFactory<Program> _factory;

    public GameControllerTests()
    {
        _context = new AppDbContext(AppDbContextOptions.GetSqlServerOptions());
        _factory = new WebApplicationFactory<Program>();
    }

    [Fact]
    public async void CreateGame_ShouldReturnBadRequest_WhenBodyIsInvalid()
    {
        var client = _factory.CreateClient();

        var dto = new CreateGameDto
        {
            Name = "ab"
        };

        var response = await client.PostAsync(ApiRoutes.GameRoutes.CreateGame, ConvertRequestHelper.ToJson(dto));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
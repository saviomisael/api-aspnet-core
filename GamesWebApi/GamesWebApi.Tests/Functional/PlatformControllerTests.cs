using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GamesWebApi.DTO;
using GamesWebApi.V1;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace GamesWebApi.Tests.Functional;

public class PlatformControllerTests : IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly AppDbContext _context;

    public PlatformControllerTests()
    {
        _factory = new WebApplicationFactory<Program>();
        _context = new AppDbContext(AppDbContextOptions.GetSqlServerOptions());
    }

    [Fact]
    public async void CreatePlatform_ShouldReturnBadRequest_WhenRequestIsInvalid()
    {
        var client = _factory.CreateClient();

        var platformDto = new CreatePlatformDto
        {
            Name = "ba"
        };

        var body = new StringContent(JsonConvert.SerializeObject(platformDto), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(ApiRoutes.PlatformRoutes.Create, body);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    public async Task InitializeAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Platforms");
    }

    public async Task DisposeAsync()
    {
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Platforms");
    }
}
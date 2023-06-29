using System.Net;
using System.Net.Http;
using System.Net.Mime;
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

    [Fact]
    public async void CreatePlatform_ShouldReturnBadRequest_WhenPlatformAlreadyExists()
    {
        _context.Platforms.Add(new Platform("xbox"));
        await _context.SaveChangesAsync();

        var client = _factory.CreateClient();

        var dto = new CreatePlatformDto { Name = "xbox" };

        var request = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await client.PostAsync(ApiRoutes.PlatformRoutes.Create, request);
        
        var body = response.Content.ReadAsStringAsync().Result;
        var errorsFromBody = JsonConvert.DeserializeObject<ErrorResponseDto>(body);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errorsFromBody.Errors.Contains("Platform xbox already exists.").Should().BeTrue();
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
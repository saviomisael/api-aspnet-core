using System.Collections.Generic;
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

        var response = await client.PostAsync(ApiRoutes.PlatformRoutes.Create, ConvertRequestHelper.ToJson(platformDto));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void CreatePlatform_ShouldReturnBadRequest_WhenPlatformAlreadyExists()
    {
        _context.Platforms.Add(new Platform("xbox"));
        await _context.SaveChangesAsync();

        var client = _factory.CreateClient();

        var dto = new CreatePlatformDto { Name = "xbox" };

        var response = await client.PostAsync(ApiRoutes.PlatformRoutes.Create, ConvertRequestHelper.ToJson(dto));
        
        var errorsFromBody = ConvertResponseHelper.ToObject<ErrorResponseDto>(response);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errorsFromBody.Errors.Contains("Platform xbox already exists.").Should().BeTrue();
    }

    [Fact]
    public async void CreatePlatform_ShouldReturnCreated_WhenRequestIsValid()
    {
        var client = _factory.CreateClient();

        var dto = new CreatePlatformDto { Name = "xbox" };

        var response = await client.PostAsync(ApiRoutes.PlatformRoutes.Create, ConvertRequestHelper.ToJson(dto));

        var platformResponse = ConvertResponseHelper.ToObject<Platform>(response);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        platformResponse.Name.Should().Be("xbox");
    }

    [Fact]
    public async void GetAll_ShouldReturnAllPlatformsFromDb()
    {
        _context.Add(new Platform("platform 1"));
        _context.Add(new Platform("platform 2"));
        _context.Add(new Platform("platform 3"));
        _context.Add(new Platform("platform 4"));
        await _context.SaveChangesAsync();
        
        var client = _factory.CreateClient();

        var response = await client.GetAsync(ApiRoutes.PlatformRoutes.GetAll);

        var platforms = ConvertResponseHelper.ToObject<ICollection<Platform>>(response);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        platforms.Count.Should().Be(4);
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
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

[Collection("Database collection")]
public class PlatformControllerTests
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly DatabaseFixture _fixture;

    public PlatformControllerTests(DatabaseFixture fixture)
    {
        _factory = new WebApplicationFactory<Program>();
        _fixture = fixture;
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
        var client = _factory.CreateClient();

        var dto = new CreatePlatformDto { Name = "platform 1" };

        var response = await client.PostAsync(ApiRoutes.PlatformRoutes.Create, ConvertRequestHelper.ToJson(dto));
        
        var errorsFromBody = ConvertResponseHelper.ToObject<ErrorResponseDto>(response);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errorsFromBody.Errors.Contains("Platform platform 1 already exists.").Should().BeTrue();
    }

    [Fact]
    public async void CreatePlatform_ShouldReturnCreated_WhenRequestIsValid()
    {
        var client = _factory.CreateClient();

        var dto = new CreatePlatformDto { Name = "xbox5000" };

        var response = await client.PostAsync(ApiRoutes.PlatformRoutes.Create, ConvertRequestHelper.ToJson(dto));

        var platformResponse = ConvertResponseHelper.ToObject<Platform>(response);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        platformResponse.Name.Should().Be("xbox5000");

        _fixture.Context.Platforms.Remove(platformResponse);
        await _fixture.Context.SaveChangesAsync();
    }

    [Fact]
    public async void GetAll_ShouldReturnAllPlatformsFromDb()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(ApiRoutes.PlatformRoutes.GetAll);

        var platforms = ConvertResponseHelper.ToObject<ICollection<Platform>>(response);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        platforms.Count.Should().Be(5);
    }

    [Fact]
    public async void DeleteByName_ShouldDeletePlatform()
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync(ApiRoutes.PlatformRoutes.DeleteByName.Replace("{name}", "xbox"));

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async void DeleteByName_ShouldReturnNotFound_WhenPlatformDoesNotExist()
    {
        var client = _factory.CreateClient();

        var response = await client.DeleteAsync(ApiRoutes.PlatformRoutes.DeleteByName.Replace("{name}", "xbox3000"));

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
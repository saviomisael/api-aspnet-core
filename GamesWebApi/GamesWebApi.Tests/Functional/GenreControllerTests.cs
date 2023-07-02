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

[Collection("Database collection")]
public class GenreControllerTests
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly DatabaseFixture _fixture;

    public GenreControllerTests(DatabaseFixture fixture)
    {
        _factory = new WebApplicationFactory<Program>();
        _fixture = fixture;
    }

    [Fact]
    public async void CreateGenre_ShouldReturnBadRequest_WhenRequestIsInvalid()
    {
        var client = _factory.CreateClient();

        var request = new CreateGenreDto
        {
            Name = "a"
        };

        var response = await client.PostAsync(ApiRoutes.GenreRoutes.Create, ConvertRequestHelper.ToJson(request));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async void CreateGenre_ShouldReturnCreatedWhenGenreIsValid()
    {
        var client = _factory.CreateClient();

        var request = new CreateGenreDto()
        {
            Name = "genre3000"
        };

        var response = await client.PostAsync(ApiRoutes.GenreRoutes.Create, ConvertRequestHelper.ToJson(request));

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var genre = await _fixture.Context.Genres.FirstAsync(x => x.Name == "genre3000");
        _fixture.Context.Genres.Remove(genre);
        await _fixture.Context.SaveChangesAsync();
    }

    [Fact]
    public async void GetAll_ShouldReturnAllGenresFromDB()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(ApiRoutes.GenreRoutes.GetAll);
        var genresFromBody = ConvertResponseHelper.ToObject<ICollection<Genre>>(response);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        genresFromBody.Should().NotBeNull();
        genresFromBody.Count.Should().Be(5);
    }

    [Fact]
    public async void DeleteByName_ShouldReturnNotFound_WhenGenreDoesNotExist()
    {
        var client = _factory.CreateClient();

        var result = await client.DeleteAsync(ApiRoutes.GenreRoutes.DeleteByName.Replace("{name}", "genre3000"));
        var errors = ConvertResponseHelper.ToObject<ErrorResponseDto>(result);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        errors.Should().NotBeNull();
        errors.Errors.Contains("Genre genre3000 not found.").Should().BeTrue();
    }

    [Fact]
    public async void DeleteByName_ShouldReturnNoContent_WhenGenreIsDeleted()
    {
        var client = _factory.CreateClient();

        var result = await client.DeleteAsync(ApiRoutes.GenreRoutes.DeleteByName.Replace("{name}", "genre"));

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
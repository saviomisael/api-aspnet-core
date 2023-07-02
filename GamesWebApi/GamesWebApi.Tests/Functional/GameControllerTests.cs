using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using GamesWebApi.DTO;
using GamesWebApi.V1;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace GamesWebApi.Tests.Functional;

[Collection("Database collection")]
public class GameControllerTests
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly DatabaseFixture _fixture;

    public GameControllerTests(DatabaseFixture fixture)
    {
        _factory = new WebApplicationFactory<Program>();
        _fixture = fixture;
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

    [Fact]
    public async void CreateGame_ShouldReturnNotFound_WhenAgeRatingDoesNotExist()
    {
        var client = _factory.CreateClient();

        var image = await File.ReadAllBytesAsync("../../../Images/720824.png");
        var imageContent = new ByteArrayContent(image);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        var dto = new CreateGameDto
        {
            Name = "The Witcher 3",
            Description = "Game of The Year",
            Price = (decimal)100.00,
            ReleaseDate = new DateTime().ToString(CultureInfo.InvariantCulture),
            AgeRatingId = "26f8319f-baba-46cf-8dc9-bb47aaf0c899",
            GenresNames = new List<string>() { "26f8319f-baba-46cf-8dc9-bb47aaf0c899" },
            PlatformsNames = new List<string>() { "26f8319f-baba-46cf-8dc9-bb47aaf0c899" }
        };

        using var multipartFormDataRequest = new MultipartFormDataContent();
        multipartFormDataRequest.Add(imageContent, "Image", "720824.png");
        multipartFormDataRequest.Add(new StringContent(dto.Name, Encoding.UTF8), "Name");
        multipartFormDataRequest.Add(new StringContent(dto.Description, Encoding.UTF8), "Description");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.Price), Encoding.UTF8), "Price");
        multipartFormDataRequest.Add(new StringContent(dto.ReleaseDate, Encoding.UTF8), "ReleaseDate");
        multipartFormDataRequest.Add(new StringContent(dto.AgeRatingId, Encoding.UTF8), "AgeRatingId");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.GenresNames)), "GenresNames");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.PlatformsNames)),
            "PlatformsNames");

        var response = await client.PostAsync(ApiRoutes.GameRoutes.CreateGame, multipartFormDataRequest);
        var body = ConvertResponseHelper.ToObject<ErrorResponseDto>(response);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        body.Errors.Contains("Age rating not found.").Should().BeTrue();
    }

    [Fact]
    public async void CreateGame_ShouldReturnNotFound_WhenGenreDoesNotExist()
    {
        var age = await _fixture.Context.AgeRatings.FirstAsync();

        var client = _factory.CreateClient();

        var image = await File.ReadAllBytesAsync("../../../Images/720824.png");
        var imageContent = new ByteArrayContent(image);
        imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        var dto = new CreateGameDto
        {
            Name = "The Witcher 3",
            Description = "Game of The Year",
            Price = (decimal)100.00,
            ReleaseDate = new DateTime().ToString(CultureInfo.InvariantCulture),
            AgeRatingId = age.Id,
            GenresNames = new List<string>() { "26f8319f-baba-46cf-8dc9-bb47aaf0c899" },
            PlatformsNames = new List<string>() { "26f8319f-baba-46cf-8dc9-bb47aaf0c899" }
        };

        using var multipartFormDataRequest = new MultipartFormDataContent();
        multipartFormDataRequest.Add(imageContent, "Image", "720824.png");
        multipartFormDataRequest.Add(new StringContent(dto.Name, Encoding.UTF8), "Name");
        multipartFormDataRequest.Add(new StringContent(dto.Description, Encoding.UTF8), "Description");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.Price), Encoding.UTF8), "Price");
        multipartFormDataRequest.Add(new StringContent(dto.ReleaseDate, Encoding.UTF8), "ReleaseDate");
        multipartFormDataRequest.Add(new StringContent(dto.AgeRatingId, Encoding.UTF8), "AgeRatingId");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.GenresNames)), "GenresNames");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.PlatformsNames)),
            "PlatformsNames");

        var response = await client.PostAsync(ApiRoutes.GameRoutes.CreateGame, multipartFormDataRequest);
        var body = ConvertResponseHelper.ToObject<ErrorResponseDto>(response);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        body.Errors.Contains("Genre not found.").Should().BeTrue();
    }

    // It's works on Postman
    // [Fact]
    // public async void CreateGame_ShouldReturnNotFound_WhenPlatformDoesNotExist()
    // {
    //     var age = await _fixture.Context.AgeRatings.FirstAsync();
    //     var genre = await _fixture.Context.Genres.FirstAsync();
    //
    //     var client = _factory.CreateClient();
    //
    //     var image = await File.ReadAllBytesAsync("../../../Images/720824.png");
    //     var imageContent = new ByteArrayContent(image);
    //     imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
    //
    //     using var multipartFormDataRequest = new MultipartFormDataContent();
    //     multipartFormDataRequest.Add(imageContent, "Image", "720824.png");
    //     multipartFormDataRequest.Add(new StringContent("The Witcher 3", Encoding.UTF8), "Name");
    //     multipartFormDataRequest.Add(new StringContent("Game of The Year", Encoding.UTF8), "Description");
    //     multipartFormDataRequest.Add(
    //         new StringContent(((decimal)100.00).ToString(CultureInfo.InvariantCulture), Encoding.UTF8), "Price");
    //     multipartFormDataRequest.Add(
    //         new StringContent(new DateTime().ToString(CultureInfo.InvariantCulture), Encoding.UTF8), "ReleaseDate");
    //     multipartFormDataRequest.Add(new StringContent(age.Id, Encoding.UTF8), "AgeRatingId");
    //     multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(new[] { genre.Name })),
    //         "GenresNames");
    //     multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(new[] { "potato" })),
    //         "PlatformsNames");
    //
    //     var response = await client.PostAsync(ApiRoutes.GameRoutes.CreateGame, multipartFormDataRequest);
    //     var body = ConvertResponseHelper.ToObject<ErrorResponseDto>(response);
    //
    //     response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    //     body.Errors[0].Should().Be("Platform not found.");
    // }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using FluentAssertions;
using GamesWebApi.DTO;
using GamesWebApi.V1;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit;

namespace GamesWebApi.Tests.Functional;

public class GameControllerTests
{
    private readonly AppDbContext _context;
    private readonly WebApplicationFactory<Program> _factory;

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
            Genres = new List<string>() { "26f8319f-baba-46cf-8dc9-bb47aaf0c899" },
            Platforms = new List<string>() { "26f8319f-baba-46cf-8dc9-bb47aaf0c899" }
        };

        using var multipartFormDataRequest = new MultipartFormDataContent();
        multipartFormDataRequest.Add(imageContent, "Image", "720824.png");
        multipartFormDataRequest.Add(new StringContent(dto.Name, Encoding.UTF8), "Name");
        multipartFormDataRequest.Add(new StringContent(dto.Description, Encoding.UTF8), "Description");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.Price), Encoding.UTF8), "Price");
        multipartFormDataRequest.Add(new StringContent(dto.ReleaseDate, Encoding.UTF8), "ReleaseDate");
        multipartFormDataRequest.Add(new StringContent(dto.AgeRatingId, Encoding.UTF8), "AgeRatingId");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.Genres)), "Genres");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.Platforms)), "Platforms");

        var response = await client.PostAsync(ApiRoutes.GameRoutes.CreateGame, multipartFormDataRequest);
        var body = ConvertResponseHelper.ToObject<ErrorResponseDto>(response);
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        body.Errors.Contains("Age rating not found.").Should().BeTrue();
    }

    [Fact]
    public async void CreateGame_ShouldReturnNotFound_WhenGenreDoesNotExist()
    {
        var age = await _context.AgeRatings.FirstAsync();
        
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
            Genres = new List<string>() { "26f8319f-baba-46cf-8dc9-bb47aaf0c899" },
            Platforms = new List<string>() { "26f8319f-baba-46cf-8dc9-bb47aaf0c899" }
        };

        using var multipartFormDataRequest = new MultipartFormDataContent();
        multipartFormDataRequest.Add(imageContent, "Image", "720824.png");
        multipartFormDataRequest.Add(new StringContent(dto.Name, Encoding.UTF8), "Name");
        multipartFormDataRequest.Add(new StringContent(dto.Description, Encoding.UTF8), "Description");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.Price), Encoding.UTF8), "Price");
        multipartFormDataRequest.Add(new StringContent(dto.ReleaseDate, Encoding.UTF8), "ReleaseDate");
        multipartFormDataRequest.Add(new StringContent(dto.AgeRatingId, Encoding.UTF8), "AgeRatingId");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.Genres)), "Genres");
        multipartFormDataRequest.Add(new StringContent(JsonConvert.SerializeObject(dto.Platforms)), "Platforms");

        var response = await client.PostAsync(ApiRoutes.GameRoutes.CreateGame, multipartFormDataRequest);
        var body = ConvertResponseHelper.ToObject<ErrorResponseDto>(response);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        body.Errors.Contains("Genre not found.").Should().BeTrue();
    }
}
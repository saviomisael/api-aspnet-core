using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using GamesWebApi.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace GamesWebApi.Tests.Functional;

public class GenreControllerTests
{
    private readonly WebApplicationFactory<Program> _factory;
    public GenreControllerTests()
    {
        _factory = new WebApplicationFactory<Program>();
    }
    
    [Fact]
    public async void CreateGenre_ShouldReturnBadRequest_WhenRequestIsInvalid()
    {
        var client = _factory.CreateClient();

        var request = new CreateGenreDto();
        request.Name = "a";

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(ApiRoutes.GenreRoutes.Create, content);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Domain.Entity;
using FluentAssertions;
using GamesWebApi.V1;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GamesWebApi.Tests.Functional;

public class AgeRatingControllerTests
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly AppDbContext _context;

    public AgeRatingControllerTests()
    {
        _factory = new WebApplicationFactory<Program>();
        _context = new AppDbContext(AppDbContextOptions.GetSqlServerOptions());
    }

    [Fact]
    public async void GetAll_ShouldReturnAllAgeRatingsSeededInDb()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(ApiRoutes.AgeRatingRoutes.GetAll);

        var bodyParsed = ConvertResponseHelper.ToObject<ICollection<AgeRating>>(response);

        var ages = bodyParsed.Select(x => x.Age);
        var descriptions = bodyParsed.Select(x => x.Description);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        ages.Contains("3+").Should().BeTrue();
        ages.Contains("7+").Should().BeTrue();
        ages.Contains("12+").Should().BeTrue();
        ages.Contains("16+").Should().BeTrue();
        ages.Contains("18+").Should().BeTrue();
        descriptions.Contains("Content suitable for ages 3 and above only.").Should().BeTrue();
        descriptions.Contains("Content suitable for ages 7 and above only.").Should().BeTrue();
        descriptions.Contains("Content suitable for ages 12 and above only.").Should().BeTrue();
        descriptions.Contains("Content suitable for ages 16 and above only.").Should().BeTrue();
        descriptions.Contains("Content suitable for ages 18 and above only.").Should().BeTrue();
    }
}
using System.Text.RegularExpressions;
using FluentAssertions;
using Infrastructure;
using Xunit;

namespace GamesWebApi.Tests.Unit;

public class RandomPasswordTests
{
    [Fact]
    public void Generate_PasswordShouldContainsAtLeastOneNumber()
    {
        var password = RandomPassword.Generate();

        var result = Regex.IsMatch(password, "[0-9]+");

        result.Should().BeTrue();
        password.Length.Should().Be(8);
    }
}
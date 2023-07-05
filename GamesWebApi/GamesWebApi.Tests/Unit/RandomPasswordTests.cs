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

    [Fact]
    public void Generate_PasswordShouldContainsAtLeastOneLowercase()
    {
        var password = RandomPassword.Generate();

        var result = Regex.IsMatch(password, "[a-z]+");

        result.Should().BeTrue();
        password.Length.Should().Be(8);
    }

    [Fact]
    public void Generate_PasswordShouldContainsAtLeastOneUppercase()
    {
        var password = RandomPassword.Generate();

        var result = Regex.IsMatch(password, "[A-Z]+");

        result.Should().BeTrue();
        password.Length.Should().Be(8);
    }
    
    [Fact]
    public void Generate_PasswordShouldContainsAtLeastOneNonAlphanumerical()
    {
        var password = RandomPassword.Generate();

        var result = Regex.IsMatch(password, @"[^a-zA-Z\d\s]");

        result.Should().BeTrue();
        password.Length.Should().Be(8);
    }
}
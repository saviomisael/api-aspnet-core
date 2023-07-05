using FluentAssertions;
using Infrastructure;
using Xunit;

namespace GamesWebApi.Tests.Unit;

public class PasswordEncrypterTests
{
    [Fact]
    public void Encrypt_ShouldEncrypt()
    {
        var hash = PasswordEncrypter.Encrypt("123aBc@#");

        hash.Should().NotBe("123aBc@#");
    }

    [Fact]
    public void Compare_ShouldReturnFalse()
    {
        var hash = PasswordEncrypter.Encrypt("123aBc@#");

        var result = PasswordEncrypter.Compare("teste123", hash);
        result.Should().BeFalse();
    }

    [Fact]
    public void Compare_ShouldReturnTrue()
    {
        var hash = PasswordEncrypter.Encrypt("123aBc@#");

        var result = PasswordEncrypter.Compare("123aBc@#", hash);

        result.Should().BeTrue();
    }
}
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
}
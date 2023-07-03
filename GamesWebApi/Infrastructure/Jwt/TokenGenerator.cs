using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTO;
using Infrastructure.Jwt.DTO;
using Infrastructure.Jwt.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Jwt;

public class TokenGenerator
{
    private readonly JwtOptions _options;

    public TokenGenerator(JwtOptions options)
    {
        _options = options;
    }
    
    public ReviewerTokenDto GenerateToken(string reviewerId, string reviewerUsername)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, reviewerId), new Claim("UserName", reviewerUsername),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(30);

        var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, DateTime.UtcNow, expiration, credentials);

        return new ReviewerTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpirationUtcTime = expiration,
            Username = reviewerUsername
        };
    }

    public PayloadDto DecodeToken(string token)
    {
        var tokenDecoded = new JwtSecurityToken(token);

        return new PayloadDto
        {
            Sub = tokenDecoded.Payload.Sub,
            UserName = tokenDecoded.Claims.First(x => x.Type == "UserName").Value
        };
    }
}
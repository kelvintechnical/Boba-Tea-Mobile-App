using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BobaTeaApp.Api.Entities;
using BobaTeaApp.Shared.Configurations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace BobaTeaApp.Api.Services;

public sealed class TokenService
{
    private readonly JwtOptions _options;

    public TokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public (string accessToken, DateTimeOffset expiresAt) GenerateAccessToken(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTimeOffset.UtcNow.AddMinutes(_options.AccessTokenMinutes);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: BuildClaims(user),
            expires: expires.UtcDateTime,
            signingCredentials: credentials);

        var handler = new JwtSecurityTokenHandler();
        return (handler.WriteToken(token), expires);
    }

    private static IEnumerable<Claim> BuildClaims(ApplicationUser user)
    {
        yield return new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString());
        yield return new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty);
        yield return new Claim("name", user.FullName ?? string.Empty);
    }
}

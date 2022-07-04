using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Serrvices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions <JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(Guid UserId, string FirstName, string LastName)
    {
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),SecurityAlgorithms.HmacSha256);
        var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Sub,UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName,LastName)
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
             audience: _jwtSettings.Audience,
            expires:_dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials
            );
        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }
}
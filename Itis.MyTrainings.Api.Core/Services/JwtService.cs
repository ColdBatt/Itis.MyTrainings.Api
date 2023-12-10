using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Itis.MyTrainings.Api.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Itis.MyTrainings.Api.Core.Services;

/// <inheritdoc />
public class JwtService: IJwtService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="configuration">Конфигурация проекта</param>
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    // Todo при добавлении ролевой добавить роль пользователя в claims
    /// <inheritdoc />
    public string GenerateJwt(Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityKey = Encoding.ASCII.GetBytes(
            _configuration["JwtSettings:SecretKey"]!);
        var claims = new List<Claim>
        {
            new(nameof(ClaimTypes.NameIdentifier), userId.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration["JwtSettings:Audience"],
            Issuer = _configuration["JwtSettings:Issuer"],
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(
                _configuration["JwtSettings:AccessTokenLifetimeInMinutes"]!)),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(jwtSecurityKey),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
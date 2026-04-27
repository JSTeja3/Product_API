using Product_API.Services.Interface;
using Product_API.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Product_API.Models;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly ITokenRepository _repo;

    public TokenService(IConfiguration config, ITokenRepository repo)
    {
        _config = config;
        _repo = repo;
    }

    public string GenerateAccessToken(string userId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, userId),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenandSaveAsync(string userId)
    {
        var bytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        var refreshToken = Convert.ToBase64String(bytes);


        await _repo.SaveTokenAsync(new RefreshToken
        {
            UserId = userId,
            Token = refreshToken,
            Expiry = DateTime.UtcNow.AddDays(1)
        });

        return refreshToken;
    }
    public async Task<bool> ValidateRefreshToken(string userId, string token)
    {
        var stored = await _repo.GetTokenAsync(userId);

        if (stored == null)
            return false;

        if (stored.Token != token)
            return false;

        if (stored.Expiry < DateTime.UtcNow)
            return false;

        return true;
    }

}
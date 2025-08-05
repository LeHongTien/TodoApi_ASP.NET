using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApi.Configurations;
using TodoApi.Contracts.Repositories;
using TodoApi.DTOs;
using TodoApi.Entities;

public class AuthService : IAuthService
{
    private readonly IRepositoryManager _repository;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IRepositoryManager repository, IOptions<JwtSettings> options)
    {
        _repository = repository;
        _jwtSettings = options.Value;
    }

    public async Task<bool> RegisterAsync(UserRegisterDTO registerDto)
    {
        var existingUser = await _repository.User.GetByUsernameAsync(registerDto.Username);
        if (existingUser != null)
            return false;

        var newUser = new User
        {
            Username = registerDto.Username,
            Password = registerDto.Password 
        };

        _repository.User.Create(newUser);
        await _repository.SaveAsync();

        return true;
    }

    public async Task<AuthResponse?> LoginAsync(UserLoginDTO loginDto)
    {
        var user = await _repository.User.GetByUsernameAsync(loginDto.Username);

        if (user == null || user.Password != loginDto.Password)
            return null;

        return new AuthResponse
        {
            Token = GenerateJwtToken(user),
            Username = user.Username       
        };

    }
    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

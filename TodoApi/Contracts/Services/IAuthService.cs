using TodoApi.DTOs;
using TodoApi.Entities;

public interface IAuthService
{
    Task<bool> RegisterAsync(UserRegisterDTO registerDto);
    Task<AuthResponse?> LoginAsync(UserLoginDTO loginDto);
}

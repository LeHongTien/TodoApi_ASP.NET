using TodoApi.Models;

namespace TodoApi.Services
{
    public interface IUserService
    {
        Task<bool> Register(User user);
        Task<bool> Login(UserDTO user);
    }
}

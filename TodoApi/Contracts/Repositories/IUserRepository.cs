using TodoApi.Entities;

namespace TodoApi.Contracts.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}

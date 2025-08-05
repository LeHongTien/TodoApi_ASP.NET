using Microsoft.EntityFrameworkCore;
using TodoApi.Contracts.Repositories;
using TodoApi.Data;
using TodoApi.Entities;

namespace TodoApi.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TodoContext context) : base(context)
        {
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await FindByCondition(u => u.Username.Equals(username))
                .FirstOrDefaultAsync();
        }
    }
}

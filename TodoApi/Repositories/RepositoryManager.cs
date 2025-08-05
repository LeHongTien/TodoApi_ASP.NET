using TodoApi.Data;
using TodoApi.Contracts.Repositories;

namespace TodoApi.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly TodoContext _context;
        private readonly ITodoRepository _todoRepository;
        private readonly IUserRepository _userRepository;
        public RepositoryManager(TodoContext context)
        {
            _context = context;
            _todoRepository = new TodoRepository(_context);
            _userRepository = new UserRepository(_context);
        }

        public ITodoRepository Todo => _todoRepository;
        public IUserRepository User => _userRepository;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}

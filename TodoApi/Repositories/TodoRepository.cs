using Microsoft.EntityFrameworkCore;
using TodoApi.Contracts.Repositories;
using TodoApi.Data;
using TodoApi.Entities;

namespace TodoApi.Repositories
{
    public class TodoRepository : RepositoryBase<TodoItem>, ITodoRepository
    {
        public TodoRepository(TodoContext context) : base(context) { }

        public async Task<TodoItem?> GetByIdAsync(long id) =>
            await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == id);
    }
}

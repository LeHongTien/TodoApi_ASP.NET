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

        public async Task<IEnumerable<Tag>> GetTagsByTodoIdAsync(long todoItemId)
        {
            return await _context.Tags
                .Where(t => t.TodoItems.Any(todo => todo.Id == todoItemId))
                .ToListAsync();
        }
    }
}

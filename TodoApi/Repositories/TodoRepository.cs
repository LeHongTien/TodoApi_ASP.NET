using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync() =>
            await _context.TodoItems.ToListAsync();

        public async Task<TodoItem?> GetByIdAsync(long id) =>
            await _context.TodoItems.FindAsync(id);

        public async Task AddAsync(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
        }

        public async Task UpdateAsync(TodoItem item)
        {
            await _context.TodoItems
                .Where(t => t.Id == item.Id)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(t => t.Name, item.Name)
                    .SetProperty(t => t.IsComplete, item.IsComplete));
            //_context.Entry(item).State = EntityState.Modified;
        }

        public async Task DeleteAsync(TodoItem item)
        {
            await _context.TodoItems
                .Where(t => t.Id == item.Id)
                .ExecuteDeleteAsync();
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }
    }
}


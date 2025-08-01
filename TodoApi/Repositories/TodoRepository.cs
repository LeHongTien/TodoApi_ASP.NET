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
            _context.TodoItems.Add(item);
            await SaveChange();
        }

        public async Task UpdateAsync(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await SaveChange();
        }

        public async Task DeleteAsync(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            await SaveChange();
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }
    }
}


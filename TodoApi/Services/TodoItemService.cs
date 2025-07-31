using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;

        public TodoItemService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        public async Task<TodoItemDTO?> GetByIdAsync(long id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            return todo == null ? null : ItemToDTO(todo);
        }

        public async Task<TodoItemDTO?> CreateAsync(TodoItemDTO dto)
        {
            var todoItem = new TodoItem
            {
                Name = dto.Name,
                IsComplete = dto.IsComplete
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return ItemToDTO(todoItem);
        }

        public async Task<bool> UpdateAsync(long id, TodoItemDTO dto)
        {
            if (id != dto.Id)
                return false;

            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return false;

            todo.Name = dto.Name;
            todo.IsComplete = dto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) => new TodoItemDTO
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete
        };
    }
}

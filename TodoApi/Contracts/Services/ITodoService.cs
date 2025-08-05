using TodoApi.DTOs;
using TodoApi.Entities;

namespace TodoApi.Contracts.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDTO>> GetAllAsync(long userId);
        Task<TodoItemDTO?> GetByIdAsync(long id, long userId);
        Task<TodoItemDTO> CreateAsync(TodoItemDTO dto);
        Task<bool> UpdateAsync(long id, TodoItemDTO dto, long userId);
        Task<bool> DeleteAsync(long id, long userId);
        Task<IEnumerable<Tag>> GetTagsByTodoIdAsync(long todoItemId);
    }

}

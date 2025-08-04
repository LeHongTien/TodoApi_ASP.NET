using TodoApi.DTOs;

namespace TodoApi.Contracts.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDTO>> GetAllAsync();
        Task<TodoItemDTO?> GetByIdAsync(long id);
        Task<TodoItemDTO> CreateAsync(TodoItemDTO dto);
        Task<bool> UpdateAsync(long id, TodoItemDTO dto);
        Task<bool> DeleteAsync(long id);
    }
}

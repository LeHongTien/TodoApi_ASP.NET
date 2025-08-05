using TodoApi.Entities;

namespace TodoApi.Contracts.Repositories
{
    public interface ITodoRepository : IRepositoryBase<TodoItem>
    {
        Task<TodoItem?> GetByIdAsync(long id);
        Task<IEnumerable<Tag>> GetTagsByTodoIdAsync(long todoItemId);
    }
}

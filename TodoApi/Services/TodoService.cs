using AutoMapper;
using TodoApi.Contracts.Repositories;
using TodoApi.Contracts.Services;
using TodoApi.DTOs;
using TodoApi.Entities;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;

        public TodoService(IRepositoryManager repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync(long userId)
        {
            var items = _repo.Todo
                .FindByCondition(t => t.UserId == userId)
                .ToList();
            return _mapper.Map<IEnumerable<TodoItemDTO>>(items);
        }

        public async Task<TodoItemDTO?> GetByIdAsync(long id, long userId)
        {
            var entity = await _repo.Todo.GetByIdAsync(id);
            if (entity == null || entity.UserId != userId) return null;
            return _mapper.Map<TodoItemDTO>(entity);
        }

        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO dto)
        {
            var entity = _mapper.Map<TodoItem>(dto);
            _repo.Todo.Create(entity);
            await _repo.SaveAsync();
            return _mapper.Map<TodoItemDTO>(entity);
        }

        public async Task<bool> UpdateAsync(long id, TodoItemDTO dto, long userId)
        {
            var entity = await _repo.Todo.GetByIdAsync(id);
            if (entity == null || entity.UserId != userId) return false;

            _mapper.Map(dto, entity);
            _repo.Todo.Update(entity);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long id, long userId)
        {
            var entity = await _repo.Todo.GetByIdAsync(id);
            if (entity == null || entity.UserId != userId) return false;

            _repo.Todo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Tag>> GetTagsByTodoIdAsync(long todoItemId)
        {
            var tags = await _repo.Todo.GetTagsByTodoIdAsync(todoItemId);
            return tags ?? Enumerable.Empty<Tag>();
        }

    }
}

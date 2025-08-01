using AutoMapper;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            //return items.Select(ItemToDTO);
            return _mapper.Map<IEnumerable<TodoItemDTO>>(items);
        }

        public async Task<TodoItemDTO?> GetByIdAsync(long id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<TodoItemDTO>(item);
        }

        public async Task<TodoItemDTO> AddAsync(TodoItemDTO dto)
        {
            var item = new TodoItem
            {
                Name = dto.Name,
                IsComplete = dto.IsComplete
            };

            await _repository.AddAsync(item);
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task<bool> UpdateAsync(long id, TodoItemDTO dto)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return false;

            item.Name = dto.Name;
            item.IsComplete = dto.IsComplete;
            await _repository.UpdateAsync(item);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return false;

            await _repository.DeleteAsync(item);
            return true;
        }
    }
}
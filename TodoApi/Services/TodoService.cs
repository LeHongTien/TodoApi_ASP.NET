using AutoMapper;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            var items = await _todoRepository.GetAllAsync();
            //return items.Select(ItemToDTO);
            return _mapper.Map<IEnumerable<TodoItemDTO>>(items);
        }

        public async Task<TodoItemDTO?> GetByIdAsync(long id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<TodoItemDTO>(item);
        }

        public async Task<TodoItemDTO> AddAsync(TodoItemDTO dto)
        {
            var item = new TodoItem
            {
                Name = dto.Name,
                IsComplete = dto.IsComplete
            };

            await _todoRepository.AddAsync(item);
            await _todoRepository.SaveChange();
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task<bool> UpdateAsync(long id, TodoItemDTO dto)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            if (item == null) return false;

            item.Name = dto.Name;
            item.IsComplete = dto.IsComplete;
            await _todoRepository.UpdateAsync(item);
            await _todoRepository.SaveChange();
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            if (item == null) return false;

            await _todoRepository.DeleteAsync(item);
            await _todoRepository.SaveChange();
            return true;
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using TodoApi.Contracts.Services;
using TodoApi.DTOs;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoItemsController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var todo = await _service.GetByIdAsync(id);
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItemDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TodoItemDTO dto) =>
            await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) =>
            await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApi.Contracts.Services;
using TodoApi.DTOs;

namespace TodoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/todos")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoItemsController(ITodoService service)
        {
            _service = service;
        }

        private long GetUserId() =>
            long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            var todos = await _service.GetAllAsync(userId);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var userId = GetUserId();
            var todo = await _service.GetByIdAsync(id, userId);
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItemDTO dto)
        {
            var userId = GetUserId();
            dto.UserId = userId;
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TodoItemDTO dto)
        {
            var userId = GetUserId();
            var updated = await _service.UpdateAsync(id, dto, userId);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var userId = GetUserId();
            var deleted = await _service.DeleteAsync(id, userId);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("{id}/tags")]
        public async Task<IActionResult> GetTagsForTodo(long id)
        {
            var tags = await _service.GetTagsByTodoIdAsync(id);
            return Ok(tags);
        }

    }
}

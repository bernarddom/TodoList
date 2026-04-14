using API.Data;
using API.DTOs.Item;
using API.Entities;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/lists/{listId}/items")]
    [ApiController]
    public class TodoItemController(AppDbContext _context) : ControllerBase
    {
        // [HttpGet]
        // public IActionResult GetItems(int listId) { }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewItemDto>> GetItem(int listId, int id)
        {
            var item = await _context.TodoItems
                .Where(i => i.ListId == listId && i.Id == id)
                .Select(i => new ViewItemDto
                {
                    Id = i.Id,
                    Value = i.Value,
                    Checkmark = i.Checkmark
                })
                .FirstOrDefaultAsync();

            if (item == null)
                return NotFound(new { message = "Item not found" });

            return Ok(item);

            // var todoList = await _context.TodoLists
            //     .Include(t => t.TodoItems)
            //     .FirstOrDefaultAsync(t => t.Id == listId);

            // if (todoList == null)
            //     return NotFound(new { message = "List not found" });

            // var todoItem = _context.TodoItems.FirstOrDefault(i => i.Id == id);

            // return
        }

        [HttpGet("")]
        public async Task<ActionResult<ViewItemDto>> GetItems(int listId)
        {
            var item = await _context.TodoItems
                .Where(i => i.ListId == listId)
                .Select(ViewItemMapper.ToDto)
                .ToListAsync();

            if (item == null)
                return NotFound(new { message = "Item not found" });

            return Ok(item);

            // var todoList = await _context.TodoLists
            //     .Include(t => t.TodoItems)
            //     .FirstOrDefaultAsync(t => t.Id == listId);

            // if (todoList == null)
            //     return NotFound(new { message = "List not found" });

            // var todoItem = _context.TodoItems.FirstOrDefault(i => i.Id == id);

            // return
        }

        [HttpPost]
        public async Task<ActionResult<ViewItemDto>> CreateItem(int listId, [FromBody] CreateItemDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var todoListExists = await _context.TodoLists
                .AnyAsync(t => t.Id == listId);
            if (!todoListExists)
                return NotFound(new { message = "List not found" });

            var todoItem = new TodoItem
            {
                Value = dto.Value,
                Checkmark = false,
                ListId = listId
            };
            // todoList?.TodoItems?.Add(todoItem);
            _context.TodoItems.Add(todoItem);

            await _context.SaveChangesAsync();

            var responseDto = new ViewItemDto
            {
                Id = todoItem.Id,
                Value = todoItem.Value,
                Checkmark = todoItem.Checkmark
            };

            return CreatedAtAction(
                nameof(GetItem),
                new { listId = listId, id = todoItem.Id },
                responseDto
            );
        }

        //     [HttpPut("{id}")]
        //     public IActionResult UpdateItem(int listId, int id, [FromBody] UpdateTodoItemDto dto) { }

        //     [HttpDelete("{id}")]
        //     public IActionResult DeleteItem(int listId, int id) { }
    }
}

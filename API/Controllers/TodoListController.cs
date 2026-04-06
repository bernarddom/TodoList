using API.Data;
using API.DTOs;
using API.DTOs.Item;
using API.DTOs.List;
using API.Entities;
using API.Mappers.List;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [Route("lists/{listId}/items")]
    [Route("api/lists/")]
    [ApiController]
    public class TodoListController(AppDbContext _context) : BaseApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<TodoList>> GetLists(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            var query = _context.TodoLists.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .AsNoTracking() // 🔥 important for performance (read-only)
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ViewListMapper.ToDto())
                .ToListAsync();


            return Ok(new
            {
                pageNumber,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                data = items
            });
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TodoList>> CreateList(CreateListDto dto)
        {
            var todoList = new TodoList
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.TodoLists.Add(todoList);
            await _context.SaveChangesAsync();

            return todoList;
        }

        [HttpGet]
        [Route("{listId}")]
        public async Task<ActionResult<ViewListDto>> ViewList(int listId)
        {
            var todoList = await _context.TodoLists.FindAsync(listId);

            if (todoList == null) return NotFound();

            // var todoItems = todoList.TodoItems?.Select(
            //     item => new ViewItemDto{
            //         Value = item.Value,
            //         Checkmark = item.Checkmark
            //     }
            // ).ToList();

            var todoListDto = new ViewListDto
            {
                Name = todoList.Name,
                Description = todoList.Description == null ? "" : todoList.Description,
                // Items = todoItems
            };

            return Ok(todoList);
        }
    }
}

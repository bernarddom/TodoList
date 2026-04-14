using API.Data;
using API.DTOs;
using API.DTOs.Item;
using API.DTOs.List;
using API.Entities;
using API.Mappers;
using System.Linq;
using API.Mappers.List;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

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
                .Select(ViewListsMapper.ToDto())
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
            var todoList = await _context.TodoLists
                            .Where(l => l.Id == listId)
                            .Select(l => new ViewListDto
                            {
                                Id = l.Id ?? 0,
                                Name = l.Name,
                                Description = l.Description ?? "",
                                createdAt = l.CreatedAt,
                                TotalItems = l.TodoItems != null ? l.TodoItems.Count() : 0,
                                ItemsPreview = (l.TodoItems ?? new List<TodoItem>())
                                        .AsQueryable()
                                        .OrderBy(i => i.CreatedAt)
                                        .Take(10)
                                        .Select(ViewItemMapper.ToDto)
                                        .ToList()
                            })
                            .FirstOrDefaultAsync();

            if (todoList == null) return NotFound();

            // var todoItems = todoList.TodoItems?.Select(
            //     item => new ViewItemDto{
            //         Value = item.Value,
            //         Checkmark = item.Checkmark
            //     }
            // ).ToList();

            // var todoListDto = ViewListMapper.ToDto().Compile()(todoList);

            return Ok(todoList);
        }
    }
}

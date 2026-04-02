using API.Data;
using API.DTOs;
using API.DTOs.Item;
using API.DTOs.List;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // [Route("lists/{listId}/items")]
    [Route("api/lists/")]
    [ApiController]
    public class TodoListController(AppDbContext context) : BaseApiController
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<TodoList>> CreateList(CreateListDto dto)
        {
            var todoList = new TodoList{
                Name = dto.Name,
                Description = dto.Description
            };

            context.TodoLists.Add(todoList);
            await context.SaveChangesAsync();

            return todoList;
        }

        [HttpGet]
        [Route("{listId}")]
        public async Task<ActionResult<ViewListDto>> ViewList(int listId)
        {
            var todoList = await context.TodoLists.FindAsync(listId);

            if ( todoList == null ) return NotFound();

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

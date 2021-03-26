using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces.ListItems;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Lists
{
    public class TodosController : CrudController<TodoItemDto, TodoItem>
    {
        private readonly ITodoItemService _todoItemService;
        public TodosController(ITodoItemService todoItemService) : base(todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpPut("toggle/{todoId}")]
        public async Task<IActionResult> ToggleTodoAsync(int todoId)
        {
            await _todoItemService.ToggleAsync(todoId);
            return Ok();
        }
    }
}

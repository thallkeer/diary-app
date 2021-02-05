using AutoMapper;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces.Lists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Lists
{
    public class TodosController : CrudController<TodoItemDto, TodoItem>
    {
        private readonly ITodoItemService _todoItemService;
        public TodosController(ITodoItemService todoItemService,IMapper mapper, ILoggerFactory loggerFactory) : base(todoItemService, mapper, loggerFactory)
        {
            _todoItemService = todoItemService;
        }

        [HttpPut("toggle/{todoID}")]
        public async Task<IActionResult> ToggleTodoAsync(int todoId)
        {
            await _todoItemService.ToggleAsync(todoId);
            return Ok();
        }
    }
}

using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiaryApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService todoService;
        private readonly IMapper mapper;

        public TodoController(ApplicationContext context, IMapper mapper)
        {
            this.todoService = new TodoService(context);
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] TodoModel todo)
        {
            var newTodo = mapper.Map<TodoItem>(todo);
            await todoService.AddItem(newTodo, todo.OwnerID);
            todo.ID = newTodo.ID;
            return Ok(todo);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoModel todoModel)
        {
            var todo = mapper.Map<TodoItem>(todoModel);
            await todoService.UpdateItem(todo);
            return Ok();
        }

        [HttpDelete("{todoID}")]
        public async Task DeleteTodo(int todoID)
        {
            await todoService.DeleteItem(todoID);
        }
    }
}
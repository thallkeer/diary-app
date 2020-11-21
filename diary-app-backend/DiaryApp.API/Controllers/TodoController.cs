using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using DiaryApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : AppBaseController<EventsController> 
    {
        private readonly ITodoService todoService;
        private readonly ListCrudContoller<TodoList, TodoItem, TodoListModel, TodoModel> crudController;

        public TodoController(ITodoService todoService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            this.crudController = new ListCrudContoller<TodoList, TodoItem, TodoListModel, TodoModel>(todoService, mapper, logger);
            this.todoService = (ITodoService)crudController.ListItemService;
        }

        [HttpPost("addTodo")]
        public async Task<IActionResult> AddTodo([FromBody] TodoModel todo)
        {
            return await crudController.AddItem(todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodoList([FromBody] TodoListModel todoListModel)
        {
            return await crudController.AddList(todoListModel);
        }

        [HttpPut("toggle/{todoID}")]
        public async Task<IActionResult> ToggleTodo(int todoId)
        {
            var todo = await todoService.GetItemByID(todoId);
            todo.Done = !todo.Done;
            await todoService.UpdateItem(todo);
            return Ok();
        }

        [HttpPut("updateTodo")]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoModel todoModel)
        {
            return await crudController.UpdateItem(todoModel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoList([FromBody] TodoListModel todoListModel)
        {
            //var todoList = await todoService.GetById(todoListModel.ID);
            //todoList.Title = todoListModel.Title;
            //await todoService.Update(todoList);
            return await crudController.UpdateList(todoListModel);
        }

        [HttpDelete("deleteTodo/{todoID}")]
        public async Task DeleteTodo(int todoID)
        {
            await crudController.DeleteItem(todoID);
        }

        [HttpDelete("{todoListID}")]
        public async Task DeleteTodoList(int todoListID)
        {
            await crudController.DeleteList(todoListID);
        }
    }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : AppBaseController<TodoController>
    {
        private readonly ITodoService todoService;

        public TodoController(ITodoService todoService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            this.todoService = todoService;
        }

        [HttpGet("{pageID}")]
        public IActionResult GetByPageID(int pageID)
        {
            try
            {
                var todoList = todoService.GetByPageID(pageID);
                if (todoList == null)
                {
                    logger.LogErrorWithDate($"Todolist not found for pageID {pageID}");
                    return NotFound();
                }
                var model = mapper.Map<TodoListModel>(todoList);
                return Ok(model);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addTodo")]
        public async Task<IActionResult> AddTodo([FromBody] TodoModel todo)
        {
            try
            {
                var newTodo = mapper.Map<TodoItem>(todo);
                await todoService.AddItem(newTodo, todo.OwnerID);
                todo.ID = newTodo.ID;
                return Ok(todo);
            }
            catch (Exception ex)
            {
                logger.LogErrorWithDate(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTodoList([FromBody] TodoListModel todoListModel)
        {
            var todoList = mapper.Map<TodoList>(todoListModel);
            await todoService.Create(todoList);
            return Ok(todoList.ID);
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
            var todo = mapper.Map<TodoItem>(todoModel);
            await todoService.UpdateItem(todo);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoList([FromBody] TodoListModel todoListModel)
        {
            var todoList = await todoService.GetById(todoListModel.ID);
            todoList.Title = todoListModel.Title;
            await todoService.Update(todoList);
            return Ok();
        }

        [HttpDelete("deleteTodo/{todoID}")]
        public async Task DeleteTodo(int todoID)
        {
            await todoService.DeleteItem(todoID);
        }

        [HttpDelete("{todoListID}")]
        public async Task DeleteTodoList(int todoListID)
        {
            await todoService.Delete(todoListID);
        }
    }
}
using AutoMapper;
using DiaryApp.Core.DTO;
using DiaryApp.Data.ServiceInterfaces.Lists;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiaryApp.API.Controllers.Lists
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : AppBaseController<TodosController>
    {
        private readonly ITodoItemService _todoItemService;
        public TodosController(ITodoItemService todoItemService,IMapper mapper, ILoggerFactory loggerFactory) : base(mapper, loggerFactory)
        {
            _todoItemService = todoItemService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TodoItemDto todo)
        {
            var id = await _todoItemService.CreateAsync(todo);
            return Ok(id);
        }

        [HttpPut("toggle/{todoID}")]
        public async Task<IActionResult> PutToggleTodoAsync(int todoId)
        {
            //TODO: fix 

            //var todo = await crudContoller.GetItemByID(todoId);
            //todo.Done = !todo.Done;
            //await todoService.UpdateItem(todo);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] TodoItemDto todoModel)
        {
            await _todoItemService.UpdateAsync(todoModel);
            return Ok();
        }

        [HttpDelete("{todoID}")]
        public async Task<IActionResult> DeleteAsync(int todoID)
        {
            await _todoItemService.DeleteAsync(todoID);
            return NoContent();
        }
    }
}

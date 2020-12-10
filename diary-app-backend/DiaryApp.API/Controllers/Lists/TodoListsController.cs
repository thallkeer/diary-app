using System.Threading.Tasks;
using AutoMapper;
using DiaryApp.Core.DTO;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListsController : AppBaseController<TodoListsController>
    {
        private readonly ITodoListService _todoListService;

        public TodoListsController(ITodoListService todoListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(mapper, loggerFactory)
        {
            _todoListService = todoListService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TodoListDto todoListModel)
        {
            var id = await _todoListService.CreateAsync(todoListModel);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] TodoListDto todoListModel)
        {
            await _todoListService.UpdateAsync(todoListModel);
            return Ok();
        }

        [HttpDelete("{todoListID}")]
        public async Task<IActionResult> DeleteAsync(int todoListID)
        {
            await _todoListService.DeleteAsync(todoListID);
            return NoContent();
        }
    }
}
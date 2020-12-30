using AutoMapper;
using DiaryApp.Data.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Data.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class TodoListsController : CrudController<TodoListDto, TodoList>
    {
        public TodoListsController(ITodoListService todoListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(todoListService, mapper, loggerFactory)
        {
        }
    }
}
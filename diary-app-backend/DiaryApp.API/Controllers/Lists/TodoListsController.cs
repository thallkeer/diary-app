using AutoMapper;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Data.DataInterfaces;
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
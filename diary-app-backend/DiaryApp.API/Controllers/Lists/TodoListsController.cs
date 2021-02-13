using AutoMapper;
using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces;
using Microsoft.Extensions.Logging;

namespace DiaryApp.API.Controllers
{
    public class TodoListsController : CrudController<TodoListDto, TodoList>
    {
        public TodoListsController(ICrudService<TodoListDto, TodoList> todoListService, IMapper mapper, ILoggerFactory loggerFactory)
            : base(todoListService, mapper, loggerFactory)
        {
        }
    }
}
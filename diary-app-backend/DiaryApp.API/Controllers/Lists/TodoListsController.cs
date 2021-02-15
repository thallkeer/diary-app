using AutoMapper;
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers
{
    public class TodoListsController : CrudController<TodoListDto, TodoList>
    {
        public TodoListsController(ICrudService<TodoListDto, TodoList> todoListService, IMapper mapper)
            : base(todoListService, mapper)
        {
        }
    }
}
using DiaryApp.Services.DTO;
using DiaryApp.Core.Entities;
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Services.DataInterfaces;

namespace DiaryApp.API.Controllers
{
    public class TodoListsController : CrudController<TodoListDto, TodoList>
    {
        public TodoListsController(ICrudService<TodoListDto, TodoList> todoListService)
            : base(todoListService)
        {
        }
    }
}
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Services.DataInterfaces;
using DiaryApp.Services.DTO.Lists;

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
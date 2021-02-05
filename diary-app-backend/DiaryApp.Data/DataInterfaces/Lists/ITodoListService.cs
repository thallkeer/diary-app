using DiaryApp.Models.DTO;
using DiaryApp.Core.Entities;

namespace DiaryApp.Data.DataInterfaces
{
    public interface ITodoListService : ICrudService<TodoListDto, TodoList>
    {
    }
}

using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;

namespace DiaryApp.Data.ServiceInterfaces.Lists
{
    public interface ITodoItemService : ICrudService<TodoItemDto, TodoItem>
    {
    }
}

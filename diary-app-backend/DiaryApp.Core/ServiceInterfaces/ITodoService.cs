using DiaryApp.Core.Models;

namespace DiaryApp.Core
{
    public interface ITodoService : IListService<TodoList, TodoItem>
    {
    }
}

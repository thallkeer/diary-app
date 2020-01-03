using DiaryApp.Core;

namespace DiaryApp.Data.Services
{
    public class TodoService : ListService<TodoList,TodoItem>, ITodoService
    {
        public TodoService(ApplicationContext context) : base(context)
        {
        }
    }
}

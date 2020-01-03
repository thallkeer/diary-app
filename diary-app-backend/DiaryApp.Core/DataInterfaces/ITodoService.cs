namespace DiaryApp.Core
{
    public interface ITodoService : ICrudService<TodoList>, IListService<TodoList, TodoItem>
    {
    }
}

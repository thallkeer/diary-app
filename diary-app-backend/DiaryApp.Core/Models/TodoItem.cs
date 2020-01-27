namespace DiaryApp.Core
{
    public class TodoItem : ListItemBase<TodoList>
    {
        public bool Done { get; set; } = false;
    }
}

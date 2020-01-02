namespace DiaryApp.Core.Models
{
    public class TodoItem : ListItemBase<TodoList>
    {
        public bool Done { get; set; } = false;
        
    }
}

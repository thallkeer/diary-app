namespace DiaryApp.Core.Models
{
    public class TodoList : DiaryList<TodoItem>
    {
        public TodoList()
        {

        }

        public TodoList(string title) : base(title)
        {
        }
    }
}

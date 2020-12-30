
namespace DiaryApp.Core.Models
{
    public class TodoList : DiaryList<TodoItem>
    {
        public TodoList() : base()
        {

        }

        public TodoList(string title) : base(title)
        {
        }
    }
}

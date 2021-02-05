
namespace DiaryApp.Core.Entities
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

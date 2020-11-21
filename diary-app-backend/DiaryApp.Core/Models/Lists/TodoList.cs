using DiaryApp.Core.Models.Lists;

namespace DiaryApp.Core.Models
{
    public class TodoList : ListBase<TodoItem>
    {
        public TodoList()
        {

        }

        public TodoList(string title) : base(title)
        {
        }
    }
}

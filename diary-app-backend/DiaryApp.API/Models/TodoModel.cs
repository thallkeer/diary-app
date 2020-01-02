using DiaryApp.Core.Models;

namespace DiaryApp.API.Models
{
    public class TodoModel : ListItemBase<TodoListModel>
    {
        public bool Done { get; set; }
    }
}

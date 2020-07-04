using DiaryApp.Core.Models.Lists;
using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Core.Models
{
    public class TodoList : ListBase<TodoItem>
    {
        public int? PurchasesAreaID { get; set; }
        public virtual PurchasesArea PurchasesArea { get; set; }

        public TodoList()
        {

        }

        public TodoList(string title, PageBase page) : base(title, page)
        {
        }
    }
}

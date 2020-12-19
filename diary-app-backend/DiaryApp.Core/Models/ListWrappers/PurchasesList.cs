using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Core.Models
{
    public class PurchaseList : DiaryListWrapper<TodoList, TodoItem, PurchasesArea, MonthPage>
    {
        public PurchaseList() : base()
        {

        }
        public PurchaseList(string title) : base(title)
        {
        }
    }    
}
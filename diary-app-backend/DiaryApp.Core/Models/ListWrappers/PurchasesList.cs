using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Core.Models
{
    public class PurchaseList : DiaryAreaList<TodoList, TodoItem, PurchasesArea, MonthPage>
    {
        public PurchaseList() : base()
        {}

        public PurchaseList(string title) : base(title)
        {}
    }    
}
using DiaryApp.Core.Entities.DiaryLists;
using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Core.Entities.ListWrappers
{
    public class PurchaseList : DiaryAreaList<TodoList, TodoItem, PurchasesArea, MonthPage>
    {
        public PurchaseList() : base()
        {}

        public PurchaseList(string title) : base(title)
        {}
    }    
}
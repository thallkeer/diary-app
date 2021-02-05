namespace DiaryApp.Core.Entities
{
    public class PurchaseList : DiaryAreaList<TodoList, TodoItem, PurchasesArea, MonthPage>
    {
        public PurchaseList() : base()
        {}

        public PurchaseList(string title) : base(title)
        {}
    }    
}
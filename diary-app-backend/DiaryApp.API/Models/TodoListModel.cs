namespace DiaryApp.API.Models
{
    public class TodoListModel : ListModel<TodoModel>
    {
        public int? PurchasesAreaID { get; set; }
    }
}

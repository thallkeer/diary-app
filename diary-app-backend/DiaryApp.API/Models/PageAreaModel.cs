using System.Collections.Generic;

namespace DiaryApp.API.Models
{
    public abstract class PageAreaModel
    {
        public int ID { get; set; }
        public string Header { get; set; }
        public int PageID { get; set; }
    }
    public class PurchasesAreaModel : PageAreaModel
    {
        public List<TodoListModel> PurchasesLists { get; set; } = new List<TodoListModel>();
    }

    public class DesiresAreaModel : PageAreaModel
    {
        public List<CommonListModel> DesiresLists { get; set; } = new List<CommonListModel>();
    }

    public class IdeasAreaModel : PageAreaModel
    {
        public CommonListModel IdeasList { get; set; } = new CommonListModel();
    }
    public class GoalsAreaModel : PageAreaModel
    {
        public List<HabitsTrackerModel> GoalsLists { get; set; } = new List<HabitsTrackerModel>();
    }
}

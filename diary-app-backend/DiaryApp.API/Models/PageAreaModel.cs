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
        public List<EventListModel> DesiresLists { get; set; } = new List<EventListModel>();
    }

    public class IdeasAreaModel : PageAreaModel
    {
        public EventListModel IdeasList { get; set; } = new EventListModel();
    }
    public class GoalsAreaModel : PageAreaModel
    {
        public List<HabitsTrackerModel> GoalsLists { get; set; } = new List<HabitsTrackerModel>();
    }
}

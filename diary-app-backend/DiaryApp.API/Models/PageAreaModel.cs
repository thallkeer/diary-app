using DiaryApp.Core;
using System.Collections.Generic;

namespace DiaryApp.API.Models
{
    public class PurchasesAreaModel : PageAreaBase
    {
        public List<TodoListModel> PurchasesLists { get; set; } = new List<TodoListModel>();
    }

    public class DesiresAreaModel : PageAreaBase
    {
        public List<EventListModel> DesiresLists { get; set; } = new List<EventListModel>();
    }

    public class IdeasAreaModel : PageAreaBase
    {
        public EventListModel IdeasList { get; set; } = new EventListModel();
    }
    public class GoalsAreaModel : PageAreaBase
    {
        public List<HabitsTrackerModel> GoalsLists { get; set; } = new List<HabitsTrackerModel>();
    }
}

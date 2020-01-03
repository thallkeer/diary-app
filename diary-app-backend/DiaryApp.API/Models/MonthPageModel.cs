using System.Collections.Generic;

namespace DiaryApp.API.Models
{
    public class MonthPageModel
    {
        public List<TodoListModel> PurchasesLists { get; set; } = new List<TodoListModel>();
        public List<EventListModel> DesiresLists { get; set; } = new List<EventListModel>();
        public EventListModel IdeasList { get; set; } = new EventListModel();
        public List<HabitsTrackerModel> GoalsLists { get; set; } = new List<HabitsTrackerModel>();
    }
}

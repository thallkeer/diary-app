using System.Collections.Generic;

namespace DiaryApp.Core
{
    public class MonthPage : PageBase
    {  
        public int? PurchasesAreaID { get; set; }
        public int? DesiresAreaID { get; set; }
        public int? IdeasAreaID { get; set; }
        public int? GoalsAreaID { get; set; }
        public virtual PurchasesArea PurchasesArea { get; set; }
        public virtual DesiresArea DesiresArea { get; set; }
        public virtual IdeasArea IdeasArea { get; set; }
        public virtual GoalsArea GoalsArea { get; set; }
    }

    public class PurchasesArea : PageAreaBase
    {
        public virtual List<TodoList> PurchasesLists { get; set; } = new List<TodoList>();
    }

    public class DesiresArea : PageAreaBase
    {
        public virtual List<EventList> DesiresLists { get; set; } = new List<EventList>();
    }

    public class IdeasArea : PageAreaBase
    {
        public virtual EventList IdeasList { get; set; }
    }

    public class GoalsArea : PageAreaBase
    {
        public virtual List<HabitsTracker> GoalsLists { get; set; } = new List<HabitsTracker>();
    }
}

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

        public MonthPage()
        {

        }
        public MonthPage(int year, int month, AppUser user) : base(year, month, user)
        { }
    }

    public class PurchasesArea : PageAreaBase
    {
        public PurchasesArea()
        {

        }
        public PurchasesArea(PageBase page) : base(page, "Покупки")
        {

        }

        public virtual List<TodoList> PurchasesLists { get; set; } = new List<TodoList>();

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new PurchasesArea(page);
            newArea.PurchasesLists = new List<TodoList>();
                this.PurchasesLists?.ForEach(pl =>
                {
                    newArea.PurchasesLists.Add(new TodoList(pl));
                });
            return newArea;
        }
    }

    public class DesiresArea : PageAreaBase
    {
        public DesiresArea()
        {

        }
        public DesiresArea(PageBase page) : base(page,"В этом месяце я хочу")
        {}

        public virtual List<EventList> DesiresLists { get; set; } = new List<EventList>();

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new DesiresArea(page);
            newArea.DesiresLists = new List<EventList>();
                this.DesiresLists?.ForEach(dl =>
                {
                    newArea.DesiresLists.Add(new EventList(dl));
                });
            return newArea;
        }
    }

    public class IdeasArea : PageAreaBase
    {
        public IdeasArea()
        {

        }
        public IdeasArea(PageBase page) : base(page, "Идеи этого месяца")
        {

        }
        public virtual EventList IdeasList { get; set; }

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new IdeasArea(page);
            newArea.IdeasList = new EventList(this.IdeasList);
            return newArea;
        }
    }

    public class GoalsArea : PageAreaBase
    {
        public GoalsArea()
        {

        }
        public GoalsArea(PageBase page) : base(page, "Цели на этот месяц")
        {

        }
        public virtual List<HabitsTracker> GoalsLists { get; set; } = new List<HabitsTracker>();

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new GoalsArea(page);
            newArea.GoalsLists = new List<HabitsTracker>();
            if (this.GoalsLists != null)
                this.GoalsLists.ForEach(tracker =>
                {
                    newArea.GoalsLists.Add(new HabitsTracker(tracker));
                });
            return newArea;
        }
    }
}

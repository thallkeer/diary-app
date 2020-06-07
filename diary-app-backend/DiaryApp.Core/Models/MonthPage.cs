using System.Collections.Generic;
using System.Linq;

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

        public void CreateAreas()
        {
            this.DesiresArea = new DesiresArea(this, true);
            this.GoalsArea = new GoalsArea(this, true);
            this.PurchasesArea = new PurchasesArea(this, true);
            this.IdeasArea = new IdeasArea(this, true);
        }
    }

    public class PurchasesArea : PageAreaBase
    {
        public PurchasesArea()
        {

        }
        public PurchasesArea(PageBase page, bool needInit) : base(page, "Покупки", needInit)
        { }

        public virtual List<TodoList> PurchasesLists { get; set; } = new List<TodoList>();

        public override void AddFromOtherArea(PageAreaBase otherArea)
        {
            if (otherArea is PurchasesArea other)
            {
                var emptyLists = this.PurchasesLists.FindAll(pl => pl.Items.Count == 0);
                this.PurchasesLists.RemoveAll(pl => emptyLists.Contains(pl));
                this.PurchasesLists.AddRange(other.PurchasesLists.Select(pl => pl.CreateListBasedOnItself(this.Page)));
            }
        }

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new PurchasesArea(page, false)
            {
                PurchasesLists = new List<TodoList>()
            };
            this.PurchasesLists?.ForEach(pl =>
            {
                newArea.PurchasesLists.Add(pl.CreateListBasedOnItself(page));
            });
            return newArea;
        }

        protected override void Initialize()
        {
            PurchasesLists.AddRange(new TodoList[]
                {
                new TodoList {Title = "Название списка", Page = this.Page},
                new TodoList {Title = "Название списка", Page = this.Page}
                });
        }
    }

    public class DesiresArea : PageAreaBase
    {
        public DesiresArea()
        {

        }
        public DesiresArea(PageBase page, bool init) : base(page, "В этом месяце я хочу", init)
        { }

        public virtual List<EventList> DesiresLists { get; set; } = new List<EventList>(3);

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new DesiresArea(page, false)
            {
                DesiresLists = new List<EventList>(3)
            };
            this.DesiresLists.ForEach(dl =>
                {
                    newArea.DesiresLists.Add(dl.CreateListBasedOnItself(page));
                });
            return newArea;
        }

        public override void AddFromOtherArea(PageAreaBase otherArea)
        {
            if (otherArea is DesiresArea other)
            {
                //var emptyLists = this.DesiresLists.FindAll(pl => pl.Items.Count == 0);
                //this.DesiresLists.RemoveAll(pl => emptyLists.Contains(pl));
                for (int i = 0; i < other.DesiresLists.Count; i++)
                {
                    this.DesiresLists[i].Items.AddRange(other.DesiresLists[i].Items);
                }
            }
        }

        protected override void Initialize()
        {
            DesiresLists.AddRange(new EventList[]
                {
                new EventList {Title = "Посетить", Page = this.Page},
                new EventList {Title = "Посмотреть", Page = this.Page},
                new EventList {Title = "Прочитать", Page = this.Page},
                });
        }
    }

    public class IdeasArea : PageAreaBase
    {
        public IdeasArea()
        {

        }
        public IdeasArea(PageBase page, bool needInit) : base(page, "Идеи этого месяца", needInit)
        {

        }
        public virtual EventList IdeasList { get; set; }

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new IdeasArea(page, false)
            {
                IdeasList = this.IdeasList.CreateListBasedOnItself(page)
            };
            return newArea;
        }

        public override void AddFromOtherArea(PageAreaBase otherArea)
        {
            if (otherArea is IdeasArea other)
            {
                this.IdeasList.Items.AddRange(other.IdeasList.Items);
            }
        }

        protected override void Initialize()
        {
            IdeasList = new EventList() { Page = this.Page };
        }
    }

    public class GoalsArea : PageAreaBase
    {
        public GoalsArea()
        {

        }
        public GoalsArea(PageBase page, bool needInit) : base(page, "Цели на этот месяц", needInit)
        {

        }
        public virtual List<HabitsTracker> GoalsLists { get; set; } = new List<HabitsTracker>();

        public override void AddFromOtherArea(PageAreaBase otherArea)
        {
            if (otherArea is GoalsArea other)
            {
                if (this.GoalsLists.Count == 1 && this.GoalsLists[0].SelectedDays.Count == 0)
                    this.GoalsLists = new List<HabitsTracker>(other.GoalsLists.Count);
                this.GoalsLists.AddRange(other.GoalsLists.Select(gl => new HabitsTracker(gl)));
            }
        }

        public override PageAreaBase TransferAreaData(PageBase page)
        {
            var newArea = new GoalsArea(page, false)
            {
                GoalsLists = new List<HabitsTracker>()
            };
            this.GoalsLists?.ForEach(tracker =>
            {
                newArea.GoalsLists.Add(new HabitsTracker(tracker));
            });
            return newArea;
        }

        protected override void Initialize()
        {
            GoalsLists.Add(new HabitsTracker() { GoalName = "Название цели" });
        }
    }
}

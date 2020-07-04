using DiaryApp.Core.Models.PageAreas;

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
}

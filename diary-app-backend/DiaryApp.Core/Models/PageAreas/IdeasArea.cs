namespace DiaryApp.Core.Models.PageAreas
{
    public class IdeasArea : PageAreaBase<MonthPage>, IMonthPageArea<IdeasArea>
    {
        private const string HeaderSTR = "Идеи этого месяца";

        public virtual IdeasList IdeasList { get; set; }

        public IdeasArea()
        {

        }
        public IdeasArea(MonthPage page, bool needInit = false) : base(page, HeaderSTR, needInit)
        {

        }

        public void AddFromOtherArea(IdeasArea other)
        {
            this.IdeasList.Items.AddRange(other.IdeasList.Items);
        }

        protected override void Initialize()
        {
            IdeasList = new IdeasList(string.Empty);
        }
    }   
}

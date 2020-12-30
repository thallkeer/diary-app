namespace DiaryApp.Core.Models.PageAreas
{
    public abstract class MonthPageArea : PageAreaBase<MonthPage>
    {
        public MonthPageArea() : base()
        {}

        public MonthPageArea(MonthPage page, string header, bool withInitialization) : base(page, header, withInitialization)
        {}
    }
}

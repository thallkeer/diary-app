using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Core.Entities.PageAreas
{
    public abstract class MonthPageArea : PageAreaBase<MonthPage>
    {
        public MonthPageArea()
        {
        }

        public MonthPageArea(MonthPage page, string header, bool withInitialization) : base(page, header,
            withInitialization)
        {
        }
    }
}

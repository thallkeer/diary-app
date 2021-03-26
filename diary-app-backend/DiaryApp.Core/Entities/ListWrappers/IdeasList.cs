using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Core.Entities.ListWrappers
{
    public class IdeasList : DiaryAreaList<CommonList, ListItem, IdeasArea, MonthPage>
    {
        public IdeasList() : base()
        {}

        public IdeasList(string title) : base(title)
        {
        }
    }
}

using DiaryApp.Core.Entities.PageAreas;
using DiaryApp.Core.Entities.Pages;

namespace DiaryApp.Core.Entities.ListWrappers
{
    public class DesiresList : DiaryAreaList<CommonList, ListItem, DesiresArea, MonthPage>
    {
        public DesiresList() : base()
        {}

        public DesiresList(string diaryListTitle) : base(diaryListTitle)
        {
        }
    }
}

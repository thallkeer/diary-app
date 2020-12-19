using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Core.Models
{
    public class DesiresList : DiaryListWrapper<CommonList, ListItem, DesiresArea, MonthPage>
    {
        public DesiresList(string title) : base(title)
        {
        }
    }
}

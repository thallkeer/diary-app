using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Core.Models
{
    public class IdeasList : DiaryListWrapper<CommonList, ListItem, IdeasArea, MonthPage>
    {
        public IdeasList(string title) : base(title)
        {
        }
    }
}

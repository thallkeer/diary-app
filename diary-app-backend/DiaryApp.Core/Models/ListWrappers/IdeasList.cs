namespace DiaryApp.Core.Models
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

namespace DiaryApp.Core.Models
{
    public class DesiresList : DiaryAreaList<CommonList, ListItem, DesiresArea, MonthPage>
    {
        public DesiresList() : base()
        {}

        public DesiresList(string title) : base(title)
        {
        }
    }
}

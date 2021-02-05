namespace DiaryApp.Core.Entities
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

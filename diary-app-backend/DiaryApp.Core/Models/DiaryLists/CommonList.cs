namespace DiaryApp.Core.Models
{
    public class CommonList : DiaryList<ListItem>
    {
        public CommonList() : base()
        {
        }

        public CommonList(string title) : base(title)
        {
        }
    }
}
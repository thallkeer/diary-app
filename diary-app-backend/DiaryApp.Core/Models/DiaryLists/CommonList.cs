namespace DiaryApp.Core.Models
{
    public class CommonList : DiaryList<ListItem>
    {
        public CommonList()
        {
        }

        public CommonList(string title) : base(title)
        {
        }
    }
}
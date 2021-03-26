using DiaryApp.Core.Entities.DiaryLists;

namespace DiaryApp.Core.Entities
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
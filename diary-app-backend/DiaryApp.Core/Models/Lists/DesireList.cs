using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.Core.Models
{
    public class DesireList : CommonList
    {
        public DesireList()
        {
        }

        public DesireList(string title, PageBase page, DesiresArea desiresArea) : base(title, page)
        {
            DesiresArea = desiresArea;
        }
    }
}

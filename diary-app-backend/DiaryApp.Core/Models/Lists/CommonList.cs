using DiaryApp.Core.Models.Lists;
using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;

namespace DiaryApp.Core
{
    public class ListItem : ListItemBase
    {
        public virtual new CommonList Owner { get; set; }

        public ListItem()
        {

        }

        public ListItem(ListItem original) : base(original)
        { }

        public override ListItemBase GetCopy() => new ListItem(this);
    }

    public class CommonList : ListBase<ListItem>
    {
        public int? IdeasAreaID { get; set; }
        public virtual IdeasArea IdeasArea { get; set; }
        public int? DesiresAreaID { get; set; }
        public virtual DesiresArea DesiresArea { get; set; }

        public CommonList()
        {
        }

        public CommonList(string title, PageBase page)
        {
            Title = title;
            Page = page;
        }        
    }
}

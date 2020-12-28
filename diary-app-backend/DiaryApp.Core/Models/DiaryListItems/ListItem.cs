using DiaryApp.Core.Interfaces;

namespace DiaryApp.Core.Models
{
    public class ListItem : ListItemBase, IDiaryListItem<CommonList, ListItem>
    {
        public virtual CommonList Owner { get; set; }

        public ListItem()
        {

        }

        public ListItem(ListItem original) : base(original)
        { }

        public override ListItemBase GetCopy() => new ListItem(this);
    }
}

namespace DiaryApp.Core.Models
{
    public class ListItem : DiaryListItem
    {
        public ListItem() : base()
        {}

        public ListItem(ListItem original) : base(original)
        { }

        public virtual new CommonList Owner { get; set; }

        public override DiaryListItem GetCopy() => new ListItem(this);
    }
}

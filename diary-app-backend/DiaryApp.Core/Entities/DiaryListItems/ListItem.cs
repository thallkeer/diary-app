namespace DiaryApp.Core.Entities
{
    public class ListItem : DiaryListItem
    {
        public ListItem() : base()
        {}

        public ListItem(ListItem original) : base(original)
        { }

        public virtual CommonList Owner { get; set; }

        public override DiaryListItem GetCopy() => new ListItem(this);
    }
}

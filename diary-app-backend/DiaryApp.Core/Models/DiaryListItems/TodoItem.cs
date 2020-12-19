namespace DiaryApp.Core.Models
{
    public class TodoItem : ListItemBase
    {
        public bool Done { get; set; }
        public virtual new TodoList Owner { get; set; }

        public TodoItem()
        {

        }

        public TodoItem(TodoItem original) : base(original)
        {
            this.Done = original.Done;
        }

        public override ListItemBase GetCopy() => new TodoItem(this);
    }
}

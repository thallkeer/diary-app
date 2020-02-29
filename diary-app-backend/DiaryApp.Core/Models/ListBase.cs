using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public abstract class ListBase<T>
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int PageID { get; set; }        
        public virtual PageBase Page { get; set; }
        public virtual List<T> Items { get; set; }

        public override string ToString()
        {
            return $"{Title} {Page?.Year} {Page?.Month}";
        }
    }

    public class EventList : ListBase<EventItem>
    {
        public int? DesiresAreaID { get; set; }
        public virtual DesiresArea DesiresArea { get; set; }
        public int? IdeasAreaID { get; set; }
        public virtual IdeasArea IdeasArea { get; set; }

        public EventList()
        {

        }

        public EventList(EventList original)
        {
            this.Title = original.Title;
            this.Items = new List<EventItem>(original.Items.Count);
            original.Items?.ForEach(item =>
            {
                this.Items.Add(new EventItem
                {
                    Subject = item.Subject,
                    Url = item.Url,
                    Description = item.Description,
                    Date = item.Date
                });
            });
        }
    }

    public class TodoList : ListBase<TodoItem>
    {   
        public int? PurchasesAreaID { get; set; }
        public virtual PurchasesArea PurchasesArea { get; set; }

        public TodoList()
        {

        }

        public TodoList(TodoList original)
        {
            this.Title = original.Title;
            this.Items = new List<TodoItem>(original.Items.Count);
            original.Items?.ForEach(item =>
            {
                this.Items.Add(new TodoItem
                {
                    Done = item.Done,
                    Subject = item.Subject,
                    Url = item.Url,
                });
            });
        }
    }
}

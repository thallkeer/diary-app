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

        protected abstract ListBase<T> CreateBasedOnItself(PageBase page);
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

        protected override ListBase<EventItem> CreateBasedOnItself(PageBase page)
        {
            var eventList = new EventList
            {
                Title = this.Title,
                Items = new List<EventItem>(this.Items.Count),
                Page = page
            };
            this.Items?.ForEach(item =>
            {
                eventList.Items.Add(new EventItem
                {
                    Subject = item.Subject,
                    Url = item.Url,
                    Description = item.Description,
                    Date = item.Date
                });
            });
            return eventList; 
        }

        public EventList CreateListBasedOnItself(PageBase page) => (EventList)CreateBasedOnItself(page);
    }

    public class TodoList : ListBase<TodoItem>
    {   
        public int? PurchasesAreaID { get; set; }
        public virtual PurchasesArea PurchasesArea { get; set; }

        public TodoList()
        {

        }

        protected override ListBase<TodoItem> CreateBasedOnItself(PageBase page)
        {
            var todolist = new TodoList
            {
                Title = this.Title,
                Items = new List<TodoItem>(this.Items.Count),
                Page = page
            };
            this.Items?.ForEach(item =>
            {
                todolist.Items.Add(new TodoItem
                {
                    Done = item.Done,
                    Subject = item.Subject,
                    Url = item.Url,
                });
            });
            return todolist;
        }

        public TodoList CreateListBasedOnItself(PageBase page) => (TodoList)CreateBasedOnItself(page);
    }
}

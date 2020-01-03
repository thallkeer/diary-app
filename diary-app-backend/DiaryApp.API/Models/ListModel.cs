using System.Collections.Generic;

namespace DiaryApp.API.Models
{
    public abstract class ListModel<TItem>
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<TItem> Items { get; set; } = new List<TItem>();
    }
}

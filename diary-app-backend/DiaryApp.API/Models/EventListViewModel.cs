using System.Collections.Generic;
using DiaryApp.Core.Models;

namespace DiaryApp.API.Models
{
    public class EventListViewModel : IListModel<EventViewModelLight>
    {
        public int ID { get ; set ; }
        public int PageID { get; set; }
        public string Title { get; set; }
        public int Month { get; set; }
        public List<EventViewModelLight> Items { get; set; } = new List<EventViewModelLight>();
    }
}

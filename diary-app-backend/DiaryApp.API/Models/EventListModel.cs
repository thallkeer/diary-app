namespace DiaryApp.API.Models
{
    public class EventListModel : ListModel<EventModel>
    {
        public int? DesiresAreaID { get; set; }
        public int? IdeasAreaID { get; set; }
    }
}

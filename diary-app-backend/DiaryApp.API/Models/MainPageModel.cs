namespace DiaryApp.API.Models
{
    public class MainPageModel
    {
        public int ID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        //public TodoListModel ThingsTodo { get; set; }
        //public EventListModel ImportantEvents { get; set; }

        public override string ToString()
        {
            return $"{ID} {Year} {Month}";
        }
    }
}

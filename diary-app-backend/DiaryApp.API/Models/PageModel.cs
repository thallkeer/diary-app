namespace DiaryApp.API.Models
{
    public class PageModel
    {
        public int ID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{ID} {Year} {Month}";
        }
    }
}

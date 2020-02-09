namespace DiaryApp.API.Models
{
    public class ListItemModel
    {
        public int ID { get; set; }
        public string Subject { get; set; } = string.Empty;
        public int OwnerID { get; set; }
        public string Url { get; set; }
    }
}

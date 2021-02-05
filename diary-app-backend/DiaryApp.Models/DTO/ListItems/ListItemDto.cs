namespace DiaryApp.Models.DTO
{
    public class ListItemDto : BaseDto
    {
        public string Url { get; set; }
        public string Subject { get; set; }
        public int OwnerID { get; set; }
    }
}

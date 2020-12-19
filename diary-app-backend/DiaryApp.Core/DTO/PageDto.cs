namespace DiaryApp.Core.DTO
{
    public class PageDto : BaseDto
    {
        public PageDto()
        {

        }

        public PageDto(int userID, int year, int month)
        {
            UserID = userID;
            Year = year;
            Month = month;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int UserID { get; set; }
    }
}

namespace DiaryApp.Core.DTO
{
    public class PageDto : BaseDto
    {
        public PageDto()
        {

        }

        public PageDto(int userId, int year, int month)
        {
            UserId = userId;
            Year = year;
            Month = month;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int UserId { get; set; }
    }
}

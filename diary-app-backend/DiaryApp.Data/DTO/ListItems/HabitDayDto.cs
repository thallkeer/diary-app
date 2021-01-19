namespace DiaryApp.Data.DTO
{
    public class HabitDayDto : BaseDto
    {
        public int Number { get; set; }
        public string Note { get; set; }
        public int HabitTrackerId { get; set; }
    }
}

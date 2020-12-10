namespace DiaryApp.Core.DTO
{
    public class MainPageDto : PageDto
    {
        public ImportantEventsAreaDto ImportantEvents { get; set; }
        public ImportantThingsAreaDto ImportantThings { get; set; }
    }
}

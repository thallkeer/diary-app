namespace DiaryApp.Core
{
    public interface IEventService : ICrudService<EventList>, IListService<EventList, EventItem>
    {
    }
}

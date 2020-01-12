using AutoMapper;
using DiaryApp.API.Models;
using DiaryApp.Core;

namespace DiaryApp.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TodoItem, TodoModel>();
            CreateMap<TodoModel, TodoItem>();
            CreateMap<EventItem, EventModel>();
            CreateMap<EventModel, EventItem>();
            CreateMap<EventList, EventListModel>();
            CreateMap<TodoList, TodoListModel>();
            CreateMap<MainPage, MainPageModel>();
            CreateMap<MonthPage, MonthPageModel>();
            CreateMap<PurchasesArea, PurchasesAreaModel>();
            CreateMap<DesiresArea, DesiresAreaModel>();
            CreateMap<IdeasArea, IdeasAreaModel>();
            CreateMap<GoalsArea, GoalsAreaModel>();
            CreateMap<HabitsTracker, HabitsTrackerModel>();
        }
    }
}

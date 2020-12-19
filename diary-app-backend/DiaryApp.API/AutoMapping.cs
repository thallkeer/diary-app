using AutoMapper;
using DiaryApp.Core;
using DiaryApp.Core.DTO;
using DiaryApp.Core.Models;
using DiaryApp.Core.Models.PageAreas;

namespace DiaryApp.API
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TodoItem, TodoItemDto>().ReverseMap();
            CreateMap<TodoList, TodoListDto>().ReverseMap();

            CreateMap<EventItem, EventItemDto>().ReverseMap();
            CreateMap<EventList, EventListDto>().ReverseMap();

            CreateMap<ListItem, ListItemDto>().ReverseMap();           
            CreateMap<CommonList, CommonListDto>().ReverseMap();

            CreateMap<PurchasesList, TodoListDto>().ReverseMap();

            CreateMap<MainPage, MainPageDto>().ReverseMap();
            CreateMap<MonthPage, MonthPageDto>().ReverseMap();            

            CreateMap<ImportantThingsArea, ImportantThingsAreaDto>().ReverseMap();                
            CreateMap<ImportantEventsArea, ImportantEventsAreaDto>().ReverseMap();

            CreateMap<PurchasesArea, PurchasesAreaDto>().ReverseMap();
            CreateMap<DesiresArea, DesiresAreaDto>().ReverseMap();
            CreateMap<IdeasArea, IdeasAreaDto>().ReverseMap();
            CreateMap<GoalsArea, GoalsAreaDto>().ReverseMap();
            CreateMap<HabitTracker, HabitTrackerDto>().ReverseMap();

            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<AppUser, UserWithPasswordDto>().ReverseMap();
        }
    }
}
